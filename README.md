# FIMWF
The Last FIM Workflow You Will Ever Need

Project Description 
 
This is a Forefront Identity Manager Workflow Activity that will allow a user to define the VB or C# code they want executed at run time. The Workflow will compile the code and run it in a separate app domain when its initiated (http://www.west-wind.com/presentations/dynamicCode/DynamicCode.htm). The UI will allow you to specify the input parameters, using statements, assemblies to include and where the result of the code execution will go.

This concept was first introduced at http://www.apollojack.com/2012/02/last-fim-workflow-you-will-ever-need.html and I have been given approval to make the code available to you. I hope it helps!

The source code should contain everything you need to get started. To install and begin using the FIM Workflow you will need to:

1. Rebuild the Workflow code usng the appropriate Microsoft.ResourceManagement and Microsoft.IdentityManagement.WFExtensionInterfaces 
   assemblies for your FIM environment
2. Ensure MPR is set up to allow creation/modification of AIC objects
3. Create AIC in FIM:
     Activity Name: Insight.FIM.Workflow.Activities.CustomCodeActivity
     Assembly Name: Insight.FIM.Workflow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=daa4bcd323c6d191
     Type Name: Insight.FIM.WebUI.Controls.CustomCodeActivitySettingsPart
4. Run Install.bat - installs assemblies to gac, assumes sharepoint templates folder location, performs restart of services, 
   creates C:\Installs (cant be changed) folder for dlls that must be in a known location for run time complication of custom code
5. Create a Workflow in the FIM Portal using the new Custom Code Activity
6. Create an MPR to apply Workflow

These instructions have also been provided in the Source Code in the Documentation/Readme.txt file.
