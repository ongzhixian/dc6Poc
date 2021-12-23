# VSTO Add-ins

# tldr;

Needs .NET Framework!

There is something new call server side of Office Web Add-ins (SWA?) that you can write using .NET Core and .NET 5+ 


# Requirement note

VSTO relies on the .NET Framework. COM add-ins can also be written with the .NET Framework. 
Office Add-ins cannot be created with .NET Core and .NET 5+, the latest versions of .NET. 
This is because .NET Core/.NET 5+ cannot work together with .NET Framework in the same process and may lead to add-in load failures. 
You can continue to use .NET Framework to write VSTO and COM add-ins for Office. 
Microsoft will not be updating VSTO or the COM add-in platform to use .NET Core or .NET 5+. 
You can take advantage of .NET Core and .NET 5+, including ASP.NET Core, to create the server side of Office Web Add-ins.


Add a reference to the Microsoft.Office.Tools.Word.v4.0.Utilities.dll assembly. This reference is required to programmatically add a Windows Forms control to the document later in this walkthrough.

# ThisAddIn

https://docs.microsoft.com/en-us/visualstudio/vsto/programming-vsto-add-ins?view=vs-2022


# Action Pane vs Custom Task Pane

An actions pane is a customizable Document Actions task pane that is attached to a specific Microsoft Office Word document or Microsoft Office Excel workbook. 

The actions pane differs from custom task panes. 
Custom task panes are associated with the application, not a specific document. 
You can create custom task panes in VSTO Add-ins for some Microsoft Office applications.



# Reference

https://docs.microsoft.com/en-us/visualstudio/vsto/getting-started-programming-vsto-add-ins?view=vs-2022


Desktop .NET Framework
https://stackoverflow.com/questions/47707095/visual-studio-code-for-net-framework
https://github.com/OmniSharp/omnisharp-vscode/wiki/Desktop-.NET-Framework
https://www.coderedcorp.com/blog/using-vs-code-with-a-legacy-net-project/

Add controls to a document at run time in a VSTO Add-in
https://docs.microsoft.com/en-us/visualstudio/vsto/walkthrough-adding-controls-to-a-document-at-run-time-in-a-vsto-add-in?view=vs-2022

Walkthrough: Create your first VSTO Add-in for Word
https://docs.microsoft.com/en-us/visualstudio/vsto/walkthrough-creating-your-first-vsto-add-in-for-word?view=vs-2022


https://github.com/nbelyh/VisioComAddinNet5


Office Add-ins with Visual Studio Code
https://code.visualstudio.com/docs/other/office


Content Office Add-ins
https://docs.microsoft.com/en-us/office/dev/add-ins/design/content-add-ins


Walkthrough: Simple data binding in VSTO Add-in Project
https://docs.microsoft.com/en-us/visualstudio/vsto/walkthrough-simple-data-binding-in-vsto-add-in-project?view=vs-2022


https://wordmvp.com/FAQs/Mailmerge.htm
https://gregmaxey.com/word_tip_pages/repeating_data.html


https://docs.microsoft.com/en-us/visualstudio/vsto/content-controls?view=vs-2022#DataBinding
https://docs.microsoft.com/en-us/visualstudio/vsto/how-to-add-content-controls-to-word-documents?redirectedfrom=MSDN&view=vs-2022


WdContentControlType enumeration (Word)
https://docs.microsoft.com/en-us/office/vba/api/word.wdcontentcontroltype
wdContentControlBuildingBlockGallery 	5 	Specifies a building block gallery content control.
wdContentControlCheckbox 	            8 	Specifies a checkbox content control.
wdContentControlComboBox 	            3 	Specifies a combo box content control.
wdContentControlDate 	                6 	Specifies a date content control.
wdContentControlGroup 	                7 	Specifies a group content control.
wdContentControlDropdownList 	        4 	Specifies a drop-down list content control.
wdContentControlPicture 	            2 	Specifies a picture content control.
wdContentControlRepeatingSection 	    9 	Specifies a repeating section content control.
wdContentControlRichText 	            0 	Specifies a rich-text content control.
wdContentControlText 	                1 	Specifies a text content control


How to: Add a custom task pane to an application
https://docs.microsoft.com/en-us/visualstudio/vsto/how-to-add-a-custom-task-pane-to-an-application?view=vs-2022

