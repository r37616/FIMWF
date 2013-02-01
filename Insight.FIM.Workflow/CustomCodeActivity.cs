using System;
using System.ComponentModel;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.Activities;
using Westwind.wwScripting;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Insight.FIM.Workflow.Activities
{
    public partial class CustomCodeActivity : SequenceActivity
    {
        // Create a logger for use in this class.
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object[] paramArray;
        private object[] paramArrayResolved;
        private int iteration = 0;

        #region Dependency properties

        public static DependencyProperty TheActivityNameProperty = 
            DependencyProperty.Register("TheActivityName", typeof(string), typeof(CustomCodeActivity));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public string TheActivityName
        {
            get
            {
                return base.GetValue(CustomCodeActivity.TheActivityNameProperty) as string;
            }
            set
            {
                base.SetValue(CustomCodeActivity.TheActivityNameProperty, value);
            }
        }

        public static DependencyProperty ActorIDProperty =
                    DependencyProperty.Register("ActorID", typeof(Guid), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public Guid ActorID
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.ActorIDProperty);
                return test == null ? new Guid() : new Guid(test.ToString());
            }
            set
            {
                this.SetValue(CustomCodeActivity.ActorIDProperty, value);
            }
        }

        public static DependencyProperty LanguageProperty =
                    DependencyProperty.Register("Language", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Language
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.LanguageProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.LanguageProperty, value);
            }
        }

        public static DependencyProperty ReturnTypeProperty =
                    DependencyProperty.Register("ReturnType", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String ReturnType
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.ReturnTypeProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.ReturnTypeProperty, value);
            }
        }

        public static DependencyProperty PropertyCountProperty =
                   DependencyProperty.Register("PropertyCount", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String PropertyCount
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.PropertyCountProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.PropertyCountProperty, value);
            }
        }


        public static DependencyProperty TargetProperty =
                   DependencyProperty.Register("Target", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Target
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.TargetProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.TargetProperty, value);
            }
        }

        public static DependencyProperty CodeProperty =
                  DependencyProperty.Register("Code", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Code
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.CodeProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.CodeProperty, value);
            }
        }

        public static DependencyProperty ParametersProperty =
                 DependencyProperty.Register("Parameters", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Parameters
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.ParametersProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.ParametersProperty, value);
            }
        }

        public static DependencyProperty IncludesProperty =
                 DependencyProperty.Register("Includes", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Includes
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.IncludesProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.IncludesProperty, value);
            }
        }

        public static DependencyProperty AssembliesProperty =
                 DependencyProperty.Register("Assemblies", typeof(String), typeof(CustomCodeActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Properties")]
        public String Assemblies
        {
            get
            {
                object test = this.GetValue(CustomCodeActivity.AssembliesProperty);
                return test == null ? "" : test.ToString();
            }
            set
            {
                this.SetValue(CustomCodeActivity.AssembliesProperty, value);
            }
        }

        #endregion

        #region Non-Dependency Properties

        private SequentialWorkflow parentWorkflow;
        public SequentialWorkflow ParentWorkflow
        {
            get
            {
                if (parentWorkflow == null)
                {
                    if (!SequentialWorkflow.TryGetContainingWorkflow(this, out parentWorkflow))
                    {
                        throw new InvalidOperationException("Unable to get Containing Workflow");
                    }
                }
                return parentWorkflow;
            }
        }

        private Guid? requestor;
        public Guid Requestor
        {
            get
            {
                if (requestor == null)
                {
                    requestor = ParentWorkflow.ActorId;
                }
                return (Guid)requestor;
            }
        }

        private string codeRetVal;
        public string CodeReturnValue
        {
            get
            {
                return codeRetVal;
            }
            set
            {
                codeRetVal = value;
            }
        }

        private string xPath;
        public string XPath
        {
            get
            {
                return xPath;
            }
            set
            {
                xPath = value;
            }
        }

        private string xPathResolved;
        public string XPathResolved
        {
            get
            {
                return xPathResolved;
            }
            set
            {
                xPathResolved = value;
            }
        }

        #endregion

        public CustomCodeActivity()
        {
            InitializeComponent();
        }

        #region Override Methods

        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception faultException)
        {
            // Never let an exception escape from HandleFault;
            // it'll cause an infinite faulting loop. 
            try
            {
                string msg = Ensynch.FIM.FimTools.AddSourceHeader("",
                    TheActivityName/*,
                        new KeyValuePair<string, string>("Actor ID
                            FimTools.DefaultInstance.DereferenceIDsToString(ActorID))*/);
                log.Error(msg, faultException);
            }
            catch { }

            return ActivityExecutionStatus.Closed;
        }

        #endregion

        private void _buildAndExecuteCode(object sender, EventArgs e)
        {
            wwScripting wwScript = null;

            try
            {
                //log.Info("User Context Code is running under: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);

                wwScript = new wwScripting(Language);
                wwScript.lSaveSourceCode = false;

                //TODO: figure out how to add these from the GAC or by relative path
                //It looks like we have to use a file path: http://www.databaseforum.info/25/860284.aspx
                //however GAC does have a file path behind it (i.e. C:\Windows\assembly\GAC_MSIL\wwScripting\1.0.4486.26865__e7bb1946e9e55389\wwScripting.dll)
                wwScript.cSupportAssemblyPath = "C:\\Installs\\";
                Environment.CurrentDirectory = System.IO.Path.GetTempPath();

                //TODO: fix wwScript.dll to get AppDomains working: currently fails on 371 of the 
                //CreateInstance method because the RemoteLoader dll cant be found, will need to 
                //change reference to use full assembly name, ie. RemoteLoader, Version=1.0.4406.26870, Culture=neutral, PublicKeyToken=739e02ea3855a61b
                //to pull it from the GAC.  I have already signed them for a strong name and put them there
                wwScript.CreateAppDomain("Insight.FIM.Workflow.CustomCode");  // force into AppDomain

                //loScript.AddAssembly("system.windows.forms.dll", "System.Windows.Forms");
                //loScript.AddAssembly("system.web.dll","System.Web");
                //wwScript.AddNamespace("System.Net"); 
                foreach (string ns in Includes.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //log.Info("Adding namespace: " + ns);
                    if (!string.IsNullOrEmpty(ns.Trim()))
                    {
                        wwScript.AddNamespace(ns.Trim());
                    }
                }

                foreach (string a in Assemblies.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //log.Info("Adding assembly: " + a);
                    if (!string.IsNullOrEmpty(a.Trim()))
                    {
                        wwScript.AddAssembly(a.Trim());
                    }
                }

                //user is passing in a return type, do we need to perform
                //some conversion here - No, we dont.  we just need to pass the
                //type on to the ChangeAttributeCodeActivity
                //what if we want to return a multi-value/array?
                CodeReturnValue = (string)wwScript.ExecuteCode(Code, paramArrayResolved);

                if (wwScript.bError)
                {
                    throw new Exception("Error from wwScript - " + wwScript.cErrorMsg);
                }

                log.Debug("Custom code execution complete.  Return value: " + CodeReturnValue);
            }
            catch (Exception ex)
            {
                log.Error("An exception occured while executing the custom code.  Details: " + ex.ToString());

                if (ex.InnerException != null)
                {
                    log.Error("InnerException: " + ex.InnerException.ToString());
                }

                throw;
            }
            finally
            {
                if (wwScript != null)
                {
                    wwScript.Dispose();
                }
            }

        }

        private void _whileXPathNotResolved(object sender, ConditionalEventArgs e)
        {
            e.Result = true;

            iteration++;

            if (iteration <= paramArray.Length)
            {
                if (log.IsDebugEnabled)
                {
                    log.Debug("_whileXPathNotResolved successfully called");
                    log.Debug("Inputs: Iteration                 : " + iteration.ToString());
                    log.Debug("        CurrentlyResolving        : " + paramArray[iteration - 1].ToString());
                    log.Debug("        PreviouslyResolvedValue   : " + XPathResolved);
                }

                if (iteration > 1)
                {
                    paramArrayResolved[iteration - 2] = XPathResolved;
                }

                XPath = paramArray[iteration - 1].ToString();
            }
            else
            {
                if (log.IsDebugEnabled)
                {
                    log.Debug("_whileXPathNotResolved successfully called");
                    log.Debug("Inputs: Iteration                 : " + iteration.ToString());
                    log.Debug("        CurrentlyResolving        : N/A");
                    log.Debug("        PreviouslyResolvedValue   : " + XPathResolved);
                }

                paramArrayResolved[iteration - 2] = XPathResolved;
                e.Result = false;
            }
        }

        private void _preprocessing(object sender, EventArgs e)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("_preprocessing successfully called");
                log.Debug("Inputs: Language      " + Language);
                log.Debug("        ReturnType    " + ReturnType);
                log.Debug("        PropertyCount " + PropertyCount);
                log.Debug("        Target        " + Target);
                log.Debug("        Code          " + Code);
                log.Debug("        Parameters    " + Parameters);
            }

            paramArray = Parameters.Split(new string[] { "," }, StringSplitOptions.None).ToArray<object>();
            paramArrayResolved = new object[paramArray.Length];
            XPathResolved = "";
        }

    }
}
