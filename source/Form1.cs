using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ReferenceFormat

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //轉換並複製按鈕觸發
        //呼叫許多函式，函示將會回傳該函示檢查完成並格式化的字串
        //透過回傳字串的組合，最後將得到格式化完成的字串
        //並自動複製至剪貼簿

        private void transformBTN_Click(object sender, EventArgs e)
        {
            if (authorCheck() != "") {
                reference.Text = authorCheck();
                if (paperNameCheck() != "")
                {
                    reference.Text += paperNameCheck();
                    if (publicationNameCheck() != "")
                    {
                        reference.Text += publicationNameCheck();
                        
                        reference.Text += volCheck();
                        reference.Text += noCheck();
                        reference.Text += pageCheck();
                        reference.Text += yearCheck();
                        fontChanged();
                        copyToClipboard();
                    } 
                }
            }
        }

        //作者檢查，完成後回傳作者部分的字串
        private string authorCheck() {
            string referenceString = "";
            //檢查欄位是否填寫
            if (author1.Text != "")
            {
                //使用 authorSplit 將名字由全名轉換成縮寫
                referenceString += authorSplit(author1.Text.Trim());
                
                //檢查第二作者是否填寫
                if (author2.Text != "")
                {
                    if (author3.Text == "")
                    {
                        referenceString += " and ";
                    }
                    else
                    {
                        referenceString += ", ";
                    }
                    referenceString += authorSplit(author2.Text.Trim());
                    if (author3.Text != "")
                    {
                        if (author4.Text == "")
                        {
                            referenceString += " and ";
                        }
                        else
                        {
                            referenceString += ", ";
                        }
                        referenceString += authorSplit(author3.Text.Trim());
                        if (author4.Text != "")
                        {
                            referenceString += " and " + authorSplit(author4.Text.Trim());
                        }
                    }
                }
                return referenceString;
            }
            else
            {
                MessageBox.Show("需要填入第一作者");
            }
            return "";
        }

        //轉換名字由全名轉換成縮寫，此部分僅英文名字有效果
        //轉換後回傳縮寫名字+完整姓氏
        private string authorSplit(string name) {
            string [] arr = name.Split(" ");
            if (arr[0].IndexOf(".") == -1) {
                if (arr[arr.Length - 1].IndexOf(".") == -1)
                {
                    string formatedName = "";
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        formatedName += arr[i][0].ToString().ToUpper() + ". ";
                    }
                    formatedName += arr[arr.Length - 1];
                    return formatedName;
                }
                else 
                {
                    string formatedName = arr[arr.Length-1];
                    for (int i = 1; i < arr.Length; i++) {
                        formatedName += " "+arr[i];
                    }
                }
            }
            return name;
        }

        //檢查論文名稱是否輸入
        private string paperNameCheck() {
            var textinfo = new CultureInfo("en-US", false).TextInfo;
            if (paperName.Text != "")
            {
                return ", \""+textinfo.ToTitleCase(paperName.Text.Trim())+",\"";
            }
            else {
                MessageBox.Show("需要填入論文名稱");
            }
            return "";
        }

        //檢查刊物名稱是否輸入
        private string publicationNameCheck() 
        {
            if (publicationName.Text!="") {
                if (conferenceRB.Checked)
                {
                    return " in Proceedings of the " + publicationName.Text.Trim();
                    /*
                    reference.Select(reference.Text.Length - publicationName.Text.Length- "Proceedings of the ".Length, publicationName.Text.Length + "Proceedings of the ".Length);
                    reference.SelectionFont = new Font(this.Font, FontStyle.Italic);
                    */
                }
                else
                {
                    return " "+publicationName.Text.Trim();
                }
            }
            else{
                MessageBox.Show("需要填入刊物名稱");
            }
            return "";
        }

        //檢查卷數是否輸入
        private string volCheck() {
            if (vol.Text != "")
            {
                return ", vol. " + vol.Text.Trim();
            }
            return "";
        }

        //檢查期數是否輸入
        private string noCheck() {
            if (no.Text != "") {
                return ", no. "+no.Text.Trim();
            }
            return "";
        }
        
        //檢查頁數是否輸入
        //以及輸入的是單頁數或區間
        private string pageCheck() {
            if (startPage.Text!="") {
                if (endPage.Text != "")
                {
                    return ", pp. " + startPage.Text.Trim() + "-" + endPage.Text.Trim();
                }
                else {
                    return ", p. " + startPage.Text.Trim();
                }
            }
            return "";
        }

        //檢查年份是否輸入
        private string yearCheck() {
            if (year.Text != "") {
                return ", " + year.Text.Trim();
            }
            return "";
        }

        //設定輸出文字的格式
        private void fontChanged() {
            reference.SelectAll();
            reference.SelectionFont = new Font("Times New Roman", 12);
            //根據不同的來源選擇，生成不同的格式
            if (conferenceRB.Checked)
            {
                //來源若為 conference 則執行此處
                reference.Select(reference.Text.IndexOf("Proceedings of the"), ("Proceedings of the " + publicationName.Text).Length);
            }
            else {
                //來源若為 journal 或 other 則執行此處
                reference.Select(reference.Text.IndexOf(publicationName.Text), publicationName.Text.Length);
            }
            reference.SelectionFont = new Font("Times New Roman", 12, FontStyle.Italic);
        }

        //將轉換後的文字複製至剪貼簿
        private void copyToClipboard() 
        {
            reference.SelectAll();
            Clipboard.SetText(reference.SelectedRtf,TextDataFormat.Rtf);
        }

        //清除按鈕觸發會清除所有內容
        private void clearBTN_Click(object sender, EventArgs e)
        {
            author1.Text = "";
            author2.Text = "";
            author3.Text = "";
            author4.Text = "";
            paperName.Text = "";
            publicationName.Text = "";
            vol.Text = "";
            no.Text = "";
            startPage.Text = "";
            endPage.Text = "";
            year.Text = "";
        }
    }
}
