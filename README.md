# ReferenceTool 簡介
ReferenceTool 是用於中興大學資管系碩士畢業論文的參考文獻格式化工具，
不同論文網站都會有不同的格式，直接複製貼上很開心但老闆會把研究生電到飛起來，一個一個改也會有漏網之魚。
因此製作了這個工具方便將參考文獻轉化成正確的格式。
由於是個人需求，只適用於中興大學資管系碩士畢業論文的參考文獻，

# 如何使用
ReferenceFormater 使用 C# 語言撰寫，使用前請確認系統可以運行 .NET 框架。
若不需要修改格式化方式，可以直接將 ReferenceFormater 下載至電腦，
執行裡面的 ReferenceFormat.exe 檔案即可。

![](https://i.imgur.com/MbAwCFs.png)

按照指示填好欄位，在按下轉換並複製，即可將內容格式化成符合論文需求的樣子，
並複製到剪貼簿，可以連著斜體等字型設定一起貼到指定位置。

![](https://i.imgur.com/P6duFn3.png)

# 修改格式設定
目前沒有設計設定的UI，需要自行修改程式碼。
所有設定可在 source 資料夾中的 Form1.cs 中修改。
