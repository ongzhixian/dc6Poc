# MS Office Research


What do we want?

Given a Word document, we want to define which part of the document to read which fields.



Generate a Word document (docx) using data from an XML file / Convert XML to a Word document based on a template
https://stackoverflow.com/questions/50117531/generate-a-word-document-docx-using-data-from-an-xml-file-convert-xml-to-a-w


Using XSLT and Open XML to Create a Word 2007 Document
https://docs.microsoft.com/en-us/previous-versions/office/developer/office-2007/ee872374(v=office.12)?redirectedfrom=MSDN


Walkthrough: Bind content controls to custom XML parts
https://docs.microsoft.com/en-us/visualstudio/vsto/walkthrough-binding-content-controls-to-custom-xml-parts?view=vs-2022#prerequisites


Adding the contents of the XML file to a custom XML part in the document at run time.


“Mail Merge” System for XML and Microsoft Word
https://www.codeguru.com/network/mail-merge-system-for-xml-and-microsoft-word/




VSTO
https://docs.microsoft.com/en-us/visualstudio/vsto/getting-started-programming-vsto-add-ins?view=vs-2022

VSTO relies on the .NET Framework! 
COM add-ins can also be written with the .NET Framework. 

Office Add-ins cannot be created with .NET Core and .NET 5+, the latest versions of .NET. 
This is because .NET Core/.NET 5+ cannot work together with .NET Framework in the same process and may lead to add-in load failures. 

You can continue to use .NET Framework to write VSTO and COM add-ins for Office. 
Microsoft will not be updating VSTO or the COM add-in platform to use .NET Core or .NET 5+. 
You can take advantage of .NET Core and .NET 5+, including ASP.NET Core, to create the server side of Office Web Add-ins.



Common tasks in Office programming
https://docs.microsoft.com/en-us/visualstudio/vsto/common-tasks-in-office-programming?view=vs-2022


Office project templates overview
https://docs.microsoft.com/en-us/visualstudio/vsto/office-project-templates-overview?view=vs-2022
https://docs.microsoft.com/en-us/visualstudio/vsto/office-solutions-development-overview-vsto?view=vs-2022


Document/Workbooks
The Word Document and Excel Workbook project templates provide code to get you started creating a solution that is based on a specific document or workbook. 
In these types of solutions, your code runs only when the associated document is open in Word or Excel.

vs Templates
The Word Template and Excel Template project templates behave identically to the Word Document and Excel Workbook project templates. 
However, the Word Template and Excel Template project templates makes it easy for users to create new local document or workbook copies of the customized template in your solution. 
The features in your solution are available from the new document that the user creates from the template.

Add-ins
When you create a project that is based on one of these project templates, 
the code in your solution runs when the associated application is open. 
Unlike document-level projects, your code is not associated with a single document.


Program VSTO Add-ins
https://docs.microsoft.com/en-us/visualstudio/vsto/programming-vsto-add-ins?view=vs-2022


How to automate Word to perform a client-side Mail Merge using XML from SQL Server
https://docs.microsoft.com/en-us/previous-versions/office/troubleshoot/office-developer/automate-word-mail-merge-using-xml


https://www.exactsoftware.com/docs/DocView.aspx?DocumentID=%7Bed4b07b2-d701-4430-811b-907a6d27a09f%7D&noheader=1&nosubject=1




Not so relevant but interesting 😁

Understand how the word template is merged with xml data stream – part 1 – simple structure
https://blogs.sap.com/2014/08/16/understand-how-the-word-template-is-merged-with-xml-data-stream-part-1-simple-structure/


https://docs.microsoft.com/en-us/previous-versions/office/developer/office-2007/bb448854(v=office.12)



Universal Data Link (UDL) configuration
https://docs.microsoft.com/en-us/sql/connect/oledb/help-topics/data-link-pages?view=sql-server-ver15
https://docs.microsoft.com/en-us/host-integration-server/db2oledbv/data-link-tool

Data Link API Overview
https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms718102(v=vs.85)



https://www.xelplus.com/import-data-from-web-to-excel/

Database Queries 			-- dqy rqy msqry
Office Database Connection 	odc



Microsoft Data Feed Provider (OData?)

https://www.c-sharpcorner.com/article/consume-odata-feed-with-c-sharp-client-application/
https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
https://www.odata.org/blog/how-to-use-web-api-odata-to-build-an-odata-v4-service-without-entity-framework/
https://www.odata.org/getting-started/understand-odata-in-6-steps/
https://www.pluralsight.com/blog/software-development/odata-asp-net-core

https://docs.microsoft.com/en-us/sharepoint/administration/create-an-excel-services-dashboard-using-an-odata-data-feed



## Office Add-ins

How are Office Add-ins different from COM and VSTO add-ins?

COM or VSTO add-ins are earlier Office integration solutions that run only in Office on Windows. 
Unlike COM add-ins, Office Add-ins don't involve code that runs on the user's device or in the Office client. 
For an Office Add-in, the application (for example, Excel), reads the add-in manifest and hooks up the add-in’s custom ribbon buttons 
and menu commands in the UI. 
When needed, it loads the add-in's JavaScript and HTML code, which executes in the context of a browser in a sandbox.

Office Add-ins provide the following advantages over add-ins built using VBA, COM, or VSTO.

    Cross-platform support. Office Add-ins run in Office on the web, Windows, Mac, and iPad.

    Centralized deployment and distribution. Admins can deploy Office Add-ins centrally across an organization.

    Easy access via AppSource. You can make your solution available to a broad audience by submitting it to AppSource.
    appsource.microsoft.com

    Based on standard web technology. You can use any library you like to build Office Add-ins.

https://docs.microsoft.com/en-us/office/dev/add-ins/develop/develop-overview
https://docs.microsoft.com/en-us/office/dev/add-ins/develop/develop-add-ins-vscode
https://docs.microsoft.com/en-us/office/dev/add-ins/develop/add-ins-with-angular2


Field Codes
https://documentation.help/MS-Office-Word-2003/worefINCLUDETEXT1.htm
https://c-rex.net/projects/samples/ooxml/e1/Part4/OOXML_P4_DOCX_INCLUDETEXTINCLUDETE_topic_ID0E4LH1.html
