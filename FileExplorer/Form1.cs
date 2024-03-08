namespace FileExplorer
{
    public partial class Form1 : Form
    {
        readonly TreeView treeView1;
        public Form1()
        {
            InitializeComponent();
            treeView1 = new();
            treeView1.Dock = DockStyle.Fill;
            Controls.Add(treeView1);
            treeView1.BeforeSelect += treeView1_BeforeSelect;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            // заполняем дерево дисками
            FillDriveNodes();
        }
        // событие перед раскрытием узла
        void treeView1_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            e.Node?.Nodes.Clear();
            try
            {
                if (Directory.Exists(e.Node?.FullPath))
                {
                    string[] dirs = Directory.GetDirectories(e.Node.FullPath);
                    foreach (string dir in dirs)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dir).Name);
                        FillTreeNode(dirNode, dir);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception) { }
        }
        // событие перед выделением узла
        void treeView1_BeforeSelect(object? sender, TreeViewCancelEventArgs e)
        {
            e.Node?.Nodes.Clear();
            try
            {
                if (Directory.Exists(e.Node?.FullPath))
                {
                    string[] dirs = Directory.GetDirectories(e.Node.FullPath);

                    foreach (string dir in dirs)
                    {
                        TreeNode dirNode = new TreeNode(new DirectoryInfo(dir).Name);
                        FillTreeNode(dirNode, dir);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception) { }
        }

        // получаем все диски на компьютере
        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView1.Nodes.Add(driveNode);
                }
            }
            catch (Exception) { }
        }
        // получаем дочерние узлы для определенного узла
        private static void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception) { }
        }
    }
}
