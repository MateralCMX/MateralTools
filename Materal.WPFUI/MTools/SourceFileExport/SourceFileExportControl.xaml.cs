using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Materal.WPFUI.MTools.SourceFileExport
{
    /// <summary>
    /// SourceFileExportControl.xaml 的交互逻辑
    /// </summary>
    public partial class SourceFileExportControl : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SourceFileExportControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextCode.Text = @"E:\Project\MateralTools\Project\MateralTools";
            TextTarget.Text = @"E:\Project\MateralTools\Application\Code";
        }
        /// <summary>
        /// 代码浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCodeBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (String.IsNullOrEmpty(TextCode.Text))
            {
                fbd.SelectedPath = Environment.CurrentDirectory;
            }
            else
            {
                fbd.SelectedPath = TextCode.Text;
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextCode.Text = fbd.SelectedPath;
            }
        }
        /// <summary>
        /// 目标浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTargetBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (!String.IsNullOrEmpty(TextTarget.Text))
            {
                fbd.SelectedPath = TextTarget.Text;
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextTarget.Text = fbd.SelectedPath;
            }
        }
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            BtnExport.IsEnabled = false;
            if (!String.IsNullOrEmpty(TextTarget.Text) && !String.IsNullOrEmpty(TextCode.Text))
            {
                Export();
            }
            else
            {
                MessageBox.Show("请设置代码目录和导出目录");
            }
            BtnExport.IsEnabled = true;
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            string codePath = TextCode.Text;
            string targetPath = TextTarget.Text;
            string[] names = Directory.GetDirectories(codePath);
            if (Directory.Exists(targetPath))
            {
                Directory.Move(targetPath, targetPath + DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            }
            Directory.CreateDirectory(targetPath);
            DirectoryInfo di = null;
            string newDirectoryName = string.Empty;
            string[] subNames = null;
            foreach (string name in names)
            {
                di = new DirectoryInfo(name);
                if (di.Name.Contains("MateralTools") && !di.Name.Contains("Tests"))
                {
                    newDirectoryName = targetPath + @"\MateralTools\" + di.Name.Substring("MateralTools".Length + 1);
                    Directory.CreateDirectory(newDirectoryName);
                    subNames = Directory.GetDirectories(name);
                    foreach (string subName in subNames)
                    {
                        di = new DirectoryInfo(subName);
                        if (di.Name == "Model" || di.Name == "Data" || di.Name == "Manager" || di.Name == "Content" || di.Name == "Lib" || di.Name == "Interface")
                        {
                            Copy(di.FullName, newDirectoryName, true);
                        }
                    }
                }
            }
            MessageBox.Show("导出完成", "提示");
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceFolderName">源文件夹目录</param>
        /// <param name="destFolderName">目标文件夹目录</param>
        /// <param name="overwrite">允许覆盖文件</param>
        public static void Copy(string sourceFolderName, string destFolderName, bool overwrite)
        {
            var sourceFilesPath = Directory.GetFileSystemEntries(sourceFolderName);

            for (int i = 0; i < sourceFilesPath.Length; i++)
            {
                var sourceFilePath = sourceFilesPath[i];
                var directoryName = System.IO.Path.GetDirectoryName(sourceFilePath);
                var forlders = directoryName.Split('\\');
                var lastDirectory = forlders[forlders.Length - 1];
                var dest = System.IO.Path.Combine(destFolderName, lastDirectory);

                if (File.Exists(sourceFilePath))
                {
                    var sourceFileName = System.IO.Path.GetFileName(sourceFilePath);
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }
                    File.Copy(sourceFilePath, System.IO.Path.Combine(dest, sourceFileName), overwrite);
                }
                else
                {
                    Copy(sourceFilePath, dest, overwrite);
                }
            }
        }
    }
}
