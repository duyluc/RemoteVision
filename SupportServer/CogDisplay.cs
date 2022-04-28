using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex;
using Cognex.VisionPro.ToolBlock;

namespace SupportServer
{
    public partial class CogDisplay : UserControl
    {
        private CogToolBlock mToolBlock { get; set; }
        private string MotherRecordKey { get; set; }
        private string[] SubRecordKey { get; set; }
        private ICogRecord Record { get; set; }
        public CogDisplay()
        {
            InitializeComponent();
        }

        private void MToolBlock_Ran(object sender, EventArgs e)
        {

        }

        public void SetToolBlock(CogToolBlock _tooblock)
        {
            this.mToolBlock = _tooblock;
            this.Record = this.mToolBlock.CreateLastRunRecord();
            this.MotherRecordKey = this.Record.RecordKey;
            List<string> _subRecordKey = new List<string>();
            foreach(ICogRecord subrecord in this.Record.SubRecords)
            {
                _subRecordKey.Add(subrecord.RecordKey);
            }
            this.SubRecordKey = _subRecordKey.ToArray();
            this.mToolBlock.Ran += MToolBlock_Ran;
        }
        public CogToolBlock GetToolBlock()
        {
            return this.mToolBlock;
        }

        private void UpdateRecordList()
        {
            this.cbSelectRecord.Items.Clear();
            foreach(string subrecordkey in this.SubRecordKey)
            {
                this.cbSelectRecord.Items.Add($"{this.MotherRecordKey}.{subrecordkey}");
            }
        }

    }
}
