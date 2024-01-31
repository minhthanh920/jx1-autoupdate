using System.Security.Cryptography;
using System.Text;
using VlAutoUpdateTool.Models;

namespace VlAutoUpdateTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void buttonFileSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = folderBrowserDialog.SelectedPath;
                textBoxFolderPath.Text = filePath;
                ListDirectory(treeViewPath, filePath);
            }
        }
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            if (treeView.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    node.Expand();
                }

            }

        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            directoryNode.Tag = directoryInfo.FullName;
            directoryNode.Checked = true;
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.Nodes.Add(new TreeNode(file.Name)
                {
                    Checked = true,
                    Tag = file.FullName
                });
            }
            return directoryNode;
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {

        }
        private HashSet<string> GetNodeNotChecked()
        {
            HashSet<string> paths = new HashSet<string>();
            if (treeViewPath.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeViewPath.Nodes)
                {
                    GetNodeNotChecked(node, paths);
                }

            }
            return paths;
        }
        private void GetNodeNotChecked(TreeNode node, HashSet<string> paths)
        {
            if (node.Nodes is not { Count: > 0 })
            {
                return;
            }
            foreach (TreeNode nodeItem in node.Nodes)
            {
                if (!nodeItem.Checked)
                {
                    paths.Add((string)node.Tag);
                }
                GetNodeNotChecked(nodeItem, paths);
            }
        }

        private void buttonCreateChecksum_Click(object sender, EventArgs e)
        {
            List<ChecksumModel> models = CreateChecksum();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(models);
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentPath, "Files");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fileName = Path.Combine(filePath, "checksum.txt");
            TextWriter tw = new StreamWriter(fileName, false);
            tw.WriteLine(json);
            tw.Close();
        }
        private List<ChecksumModel> CreateChecksum()
        {
            string rootPath = textBoxFolderPath.Text;
            List<ChecksumModel> models = [];
            if (treeViewPath.Nodes.Count > 0)
            {
                using (var hashAlgorithm = MD5.Create())
                {
                    foreach (TreeNode node in treeViewPath.Nodes)
                    {
                        var path = (string)node.Tag;
                        if (File.Exists(path) && node.Checked)
                        {
                            ChecksumModel model = new ChecksumModel()
                            {
                                Path = path,
                                AbsolutePath = path.Replace(rootPath, string.Empty).Replace("\\", "/").TrimStart('/'),

                            };
                            FileInfo fileInfo = new FileInfo(path);
                            model.Hash = ComputeHash(fileInfo, hashAlgorithm);
                            model.UrlPath = model.AbsolutePath.Replace("\\", "/");
                            models.Add(model);
                        }
                        CreateChecksum(rootPath, node, hashAlgorithm, models);
                    }
                }
            }
            return models;
        }
        private void CreateChecksum(string rootPath, TreeNode node, HashAlgorithm hashAlgorithm, List<ChecksumModel> models)
        {
            if (node.Nodes is not { Count: > 0 })
            {
                return;
            }
            foreach (TreeNode nodeItem in node.Nodes)
            {
                var path = (string)nodeItem.Tag;
                if (File.Exists(path) && nodeItem.Checked)
                {
                    ChecksumModel model = new ChecksumModel()
                    {
                        Path = path,
                        AbsolutePath = path.Replace(rootPath, string.Empty).Replace("\\", "/")
                    };
                    FileInfo fileInfo = new FileInfo(path);
                    model.Hash = ComputeHash(fileInfo, hashAlgorithm);
                    model.UrlPath = model.AbsolutePath.Replace("\\", "/");
                    models.Add(model);
                }
                CreateChecksum(rootPath, nodeItem, hashAlgorithm, models);
            }
        }
        public string ComputeHash(FileInfo fileInfo, HashAlgorithm hashAlgorithm)
        {
            using (var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var hash = hashAlgorithm.ComputeHash(fs);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        private void treeViewPath_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            var check = e.Node.Checked;
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode nodeItem in e.Node.Nodes)
                {
                    nodeItem.Checked = check;
                }
            }
        }
    }
}
