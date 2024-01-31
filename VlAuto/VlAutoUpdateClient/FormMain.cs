using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using VlAutoUpdateClient.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VlAutoUpdateClient;

public partial class FormMain : Form
{
    private bool IsRunning = false;
    private readonly IHttpClient _httpClient;
    private readonly IHttpClient _httpClientDownload;
    public FormMain(HttpClientResolver clientResolver)
    {
        InitializeComponent();
        _httpClient = clientResolver(HttpClientNameEnum.Default.ToString());
        _httpClientDownload = clientResolver(HttpClientNameEnum.Download.ToString());
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        Task.Factory.StartNew(() =>
        {
            var task = CheckSum();
        });
    }
    private async Task CheckSum()
    {
        try
        {
            if (IsRunning)
            {
                return;
            }
            IsRunning = true;
            buttonRetry.Invoke(delegate
            {
                buttonRetry.Enabled = false;
            });
            progressBarAll.Invoke(delegate
            {
                progressBarAll.Maximum = 1;
                progressBarAll.Value = 0;
            });
            progressBarCurent.Invoke(delegate
            {
                progressBarCurent.Maximum = 1;
                progressBarCurent.Value = 0;
            });
            SetStatusText(@"Đang kết nối máy chủ");
            string urlChecksum = Common.GetUrl("/Checksum");
            var json = string.Empty;
            try
            {
                var response = await _httpClient.Get(urlChecksum);
                if (!response.IsSuccessStatusCode)
                {
                    SetStatusText(@"Không thể kết nối máy chủ");
                    return;
                }
                json = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                SetStatusText(@"Không thể kết nối máy chủ");
                return;
            }

            if (string.IsNullOrEmpty(json))
            {
                SetStatusText(@"Tập tin không đúng");
                return;
            }

            ChecksumModel[]? models = Common.JsonDeserializeObject<ChecksumModel[]>(json);
            if (models is not { Length: > 0 })
            {
                SetStatusText(@"Tập tin không đúng định dạng");
                return;
            }
            SetStatusText("Đang kiếm tra tập tin");
            string folderPath = Environment.CurrentDirectory;
            string folderGamePath = Path.Combine(folderPath, "Game");
            if (!Directory.Exists(folderGamePath))
            {
                Directory.CreateDirectory(folderGamePath);
            }
            HashSet<ChecksumModel> filesDownload = [];
            using (var hashAlgorithm = MD5.Create())
            {
                foreach (var model in models)
                {
                    model.AbsolutePath = model.AbsolutePath.TrimStart('/');
                    SetStatusText("Đang kiếm tra tập tin " + model.AbsolutePath);

                    model.UrlPath = model.UrlPath.TrimStart('/');
                    string filePath = Path.Combine(folderPath, "Game", model.AbsolutePath);
                    model.Path = filePath;
                    if (!File.Exists(filePath))
                    {
                        filesDownload.Add(model);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        string hash = ComputeHash(fileInfo, hashAlgorithm);
                        if (hash != model.Hash)
                        {
                            filesDownload.Add(model);
                        }
                    }
                }
            }
            if (filesDownload.Count > 0)
            {
                progressBarAll.Invoke(delegate { progressBarAll.Maximum = filesDownload.Count; });
                SetStatusText("Cần cập nhật " + filesDownload.Count + " file");
                int i = 0;
                foreach (var model in filesDownload)
                {
                    if (string.IsNullOrEmpty(model.Path))
                    {
                        continue;
                    }
                    i++;
                    progressBarAll.Invoke(delegate { progressBarAll.Value = i; });
                    SetStatusText("Đang tải (" + i + "/" + filesDownload.Count + ") " + model.AbsolutePath);
                    await DownLoadFile(model);
                }
            }
            else
            {
                progressBarAll.Invoke(delegate
                {
                    progressBarAll.Maximum = 1;
                    progressBarAll.Value = 1;
                });
                progressBarCurent.Invoke(delegate
                {
                    progressBarCurent.Maximum = 1;
                    progressBarCurent.Value = 1;
                });
            }
            SetStatusText("Cập nhật hoàn tất");

        }
        catch (Exception ex)
        {
            SetStatusText("Có lỗi xảy ra: " + ex.Message);
        }
        finally
        {
            buttonRetry.Invoke(delegate
            {
                buttonRetry.Enabled = true;
            });
            IsRunning = false;
        }

    }
    private string ComputeHash(FileInfo fileInfo, HashAlgorithm hashAlgorithm)
    {
        using (var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            var hash = hashAlgorithm.ComputeHash(fs);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
    private async Task DownLoadFile(ChecksumModel model)
    {
        try
        {
            string? folderPath = System.IO.Path.GetDirectoryName(model.Path);
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string url = Common.GetUrl("/Game?f=" + HttpUtility.UrlEncode(model.UrlPath));
            using (FileStream fs = new FileStream(model.Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                await _httpClientDownload.DownloadDataAsync(url, fs, progressBarCurent);
            }
        }
        catch (Exception ex)
        {
            SetStatusText("Có lỗi xảy ra: " + ex.Message);
        }
    }
    private void SetStatusText(string text)
    {
        labelStatus.Invoke(delegate { labelStatus.Text = text; });
    }

    private void buttonRetry_Click(object sender, EventArgs e)
    {
        Task.Factory.StartNew(() =>
        {
            var task = CheckSum();
        });

    }

    private void buttonExit_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.Application.Exit();
    }

    private void buttonOpenGame_Click(object sender, EventArgs e)
    {
        string folderPath = Environment.CurrentDirectory;
        string folderGamePath = Path.Combine(folderPath, "Game");
        var startInfo1 = new ProcessStartInfo
        {
            FileName = "game.exe",
            Arguments = null,
            WorkingDirectory = folderGamePath,
            CreateNoWindow = false,
            UseShellExecute = true,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Verb = "runas"
        };
        using var process1 = new Process();
        process1.StartInfo = startInfo1;
        process1.Start();
    }
}

