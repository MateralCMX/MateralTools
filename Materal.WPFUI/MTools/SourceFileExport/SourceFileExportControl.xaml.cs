using MateralTools.MIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            ExportInit(targetPath);
            DirectoryInfo di = null;
            string newDirectoryName = string.Empty;
            string[] subNames = null;
            string[] names = Directory.GetDirectories(codePath);
            string projectName = "MateralTools";
            string[] codeDirectoryName = { "Model", "Data", "Manager", "Content", "Lib", "Interface" };
            foreach (string name in names)
            {
                di = new DirectoryInfo(name);
                if (di.Name.Contains(projectName) && !di.Name.Contains("Tests"))
                {
                    newDirectoryName = string.Format(@"{0}\{1}\{2}", targetPath, projectName, di.Name.Substring(projectName.Length + 1));
                    Directory.CreateDirectory(newDirectoryName);
                    subNames = Directory.GetDirectories(name);
                    foreach (string subName in subNames)
                    {
                        di = new DirectoryInfo(subName);
                        if (codeDirectoryName.Contains(di.Name))
                        {
                            IOManager.CopyDirectory(di.FullName, newDirectoryName, true);
                        }
                    }
                }
            }
            MessageBox.Show("导出完成，确定后打开资源管理器", "提示");
            IOManager.OpenExplorer(targetPath);
        }
        /// <summary>
        /// 导出前准备工作
        /// </summary>
        /// <param name="targetPath"></param>
        private void ExportInit(string targetPath)
        {
            if (Directory.Exists(targetPath))
            {
                IOManager.DeleteDirectory(targetPath);
            }
            else
            {
                Directory.CreateDirectory(targetPath);
            }
        }
    }
}
