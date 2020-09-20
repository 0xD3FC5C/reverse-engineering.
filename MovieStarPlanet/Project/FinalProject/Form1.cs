using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluorineFx.IO;
using System.IO;
using System.Net;
using DoloModeOn;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            object[] arguments = { Convert.ToInt32(textBox1.Text) };
            AMFMessage _amf = new AMFMessage(3);
            _amf.AddHeader(new AMFHeader("id", false, Hash.createChecksum(arguments)));
            _amf.AddHeader(new AMFHeader("needClassName", false, true));
            _amf.AddBody(new AMFBody("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.GetActorNameFromId", "/1",  arguments));
            MemoryStream mStream = new MemoryStream();
            AMFSerializer serialize = new AMFSerializer(mStream);
            serialize.WriteMessage(_amf);
            HttpClient client = new HttpClient();
            byte[] AMFBytes = Encoding.Default.GetBytes(Encoding.Default.GetString(mStream.ToArray()));

            ///Set referer to send AMF without 404 error
            Uri referer = new Uri("https://assets.mspcdns.com/msp/91.0.2/Main_20200728_110605.swf");
            client.DefaultRequestHeaders.Referrer = referer;

            try
            {
                string gateway = "https://ws-" + comboBox1.Text + ".mspapis.com/msp/91.0.6/Gateway.aspx?method=MovieStarPlanet.WebService.UserSession.AMFUserSessionService.GetActorNameFromId";
                var AMFHttpClientByteArray = new ByteArrayContent(AMFBytes);
                AMFHttpClientByteArray.Headers.ContentType = new MediaTypeHeaderValue("application/x-amf");

                await client.PostAsync(gateway, AMFHttpClientByteArray);
            }

            catch(WebException exception)
            {
                MessageBox.Show("Error!" + exception);
            }
        }
    }
}s
