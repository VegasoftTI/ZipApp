using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

namespace ZipApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string caminhoPastaZipar = @"c:\zip\arq";
        string caminhoZip = @"c:\zip\resultado.zip";
        string extrairPara = @"c:\zip\notzip\";



        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ZipFile.CreateFromDirectory(caminhoPastaZipar, caminhoZip);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ZipFile.ExtractToDirectory(caminhoZip, extrairPara);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream zipToOpen = new FileStream(caminhoZip, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("exemplo.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("ola!!!");
                        writer.WriteLine("sou mais um arquivo do zip");
                    }
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            using (ZipArchive archive = ZipFile.OpenRead(caminhoZip))
            {
                conteudoZip.Text = "";
                foreach (ZipArchiveEntry entry in archive.Entries.OrderBy(it => it.FullName))
                {
                    conteudoZip.Text += entry.FullName+"\n";
                    //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    entry.ExtractToFile(System.IO.Path.Combine(extrairPara, entry.FullName));
                    //}
                }
            }
        }
    }
}
