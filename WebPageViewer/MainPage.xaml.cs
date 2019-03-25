using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebPageViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //The application uses an asynchronous method from the .NET library to fetch a web page from a given URL
        private async Task<string> FetchWebPage(string url)
        {
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

        /* The method below shows how WhenAll is used, but doesn't necessarily show good programming practice.
         *  - The order of the items in the returned collection may not fetch the order of submitted site;
         *  - There is no aggregation of any exceptions;
        private async Task<IEnumerable<string>> FetchWebPages(string[] urls)
        {
            var tasks = new List<Task<string>>();

            foreach(string url in urls)
            {
                tasks.Add(FetchWebPage(url));
            }

            return await Task.WhenAll(tasks);
        } */

        /* The act of loading a web page may fail.
         * The FetchWebPage will throw an exception in this situation.
         * The await is now enclosed in a try-catch construction. If an exception is thrown during the await, 
         * it can be caught and deal with */
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusTextBlock.Text = "Loading Page...";
                ResultTextBlock.Text = await FetchWebPage(URLTextBox.Text);
                StatusTextBlock.Text = "Page Loaded";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = ex.Message;
            }
        }
    }
}
