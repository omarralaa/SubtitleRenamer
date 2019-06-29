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
using Microsoft.Win32;
using Path = System.IO.Path;

namespace SubtitleRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _videosFilesNames;
        private string[] _subtitlesFilesNames;
        
        public MainWindow()
        {
            InitializeComponent();
        }


        
        private void BrowseVideosFiles_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            var result = openFileDialog.ShowDialog();

            if (result == false) return;
            _videosFilesNames = openFileDialog.FileNames;
            VideosList.ItemsSource = _videosFilesNames;
        }

        private void BrowseSubtitlesFiles_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;        
            var result = openFileDialog.ShowDialog();

            if (result == false) return;
            _subtitlesFilesNames = openFileDialog.FileNames;
            SubtitlesList.ItemsSource = _subtitlesFilesNames;
        }


        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (_videosFilesNames.Length != _subtitlesFilesNames.Length)
                MessageBox.Show("The Number of videos and subtitles must be equal");
            else
            {
                try
                {
                    for (int i = 0; i < _videosFilesNames.Length; i++)
                    {
                        var directoryName = Path.GetDirectoryName(_videosFilesNames[i]);
                        var vidNameWithoutExtension = Path.GetFileNameWithoutExtension(_videosFilesNames[i]);
                        var subtitleExtenstion = Path.GetExtension(_subtitlesFilesNames[i]);
                        var newSubtitleName = string.Concat(directoryName, @"\", vidNameWithoutExtension,
                            subtitleExtenstion);
                        File.Move(_subtitlesFilesNames[i], newSubtitleName);
                    }

                    MessageBox.Show("Files are Renamed Successfully");
                }
                catch
                {
                    MessageBox.Show("Something Wrong Happened");
                }

            }
            

        }
    }
}
