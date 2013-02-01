using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Insight.FIM.Workflow.Activities;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;
using System.Collections;


namespace Insight.FIM.WebUI.Controls
{
    public class CustomCodeActivitySettingsPart : ActivitySettingsPart
    {
        // Create a logger for use in this class.
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ActivitySettingsPart members

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            CustomCodeActivity activity = new CustomCodeActivity();
            SaveSettingsOnActivity(activity);
            return activity;
        }

        protected void SaveSettingsOnActivity(CustomCodeActivity activity)
        {
            try
            {
                activity.TheActivityName = getControlText("txtActivityName");
                activity.ActorID = new Guid(getControlText("ddlActor"));
                activity.PropertyCount = getControlText("txtPropertyCount");
                activity.ReturnType = getControlText("ddlReturnType");
                activity.Target = getControlText("tbTarget");
                activity.Language = getControlText("ddlLanguage");
                activity.Code = getControlText("tbCode");
                activity.Includes = getControlText("txtIncludes");
                activity.Assemblies = getControlText("txtAssemblies");

                int count = 0;
                if (int.TryParse(getControlText("txtPropertyCount").ToString(), out count))
                {
                    string paramList = "";

                    for (int i = 1; i <= count; i++)
                    {
                        try
                        {
                            paramList += getControlText("Parameter" + i) + ",";
                        }
                        catch { }
                    }

                    if (paramList.EndsWith(",")) paramList = paramList.Substring(0, paramList.Length - 1);

                    setControlText("txtParamList", paramList);
                }

                activity.Parameters = getControlText("txtParamList");

            }
            catch (Exception exc)
            {
                log.Error("[SaveSettingsOnActivity]", exc);
            }
        }

        public override void LoadActivitySettings(Activity activity)
        {
            CustomCodeActivity activity2 = activity as CustomCodeActivity;
            if (activity2 != null)
            {
                setControlText("txtActivityName", activity2.TheActivityName.ToString());
                setControlText("ddlActor", activity2.ActorID.ToString());
                setControlText("txtPropertyCount", activity2.PropertyCount.ToString());
                setControlText("ddlReturnType", activity2.ReturnType.ToString());
                setControlText("tbTarget", activity2.Target.ToString());
                setControlText("ddlLanguage", activity2.Language.ToString());
                setControlText("tbCode", activity2.Code.ToString());
                setControlText("txtParamList", activity2.Parameters.ToString());
                setControlText("txtIncludes", activity2.Includes.ToString());
                setControlText("txtAssemblies", activity2.Assemblies.ToString());
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data["TheActivityName"] = getControlText("txtActivityName");
            data["ActorID"] = new Guid(getControlText("ddlActor"));
            data["PropertyCount"] = getControlText("txtPropertyCount");
            data["ReturnType"] = getControlText("ddlReturnType");
            data["Target"] = getControlText("tbTarget");
            data["Language"] = getControlText("ddlLanguage");
            data["Code"] = getControlText("tbCode");
            data["Parameters"] = getControlText("txtParamList");
            data["Includes"] = getControlText("txtIncludes");
            data["Assemblies"] = getControlText("txtAssemblies");

            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (data != null)
            {
                setControlText("txtActivityName", "" + data["TheActivityName"]);
                setControlText("ddlActor", "" + data["ActorID"].ToString());
                setControlText("txtPropertyCount", "" + data["PropertyCount"].ToString());
                setControlText("ddlReturnType", "" + data["ReturnType"].ToString());
                setControlText("tbTarget", "" + data["Target"].ToString());
                setControlText("ddlLanguage", "" + data["Language"].ToString());
                setControlText("tbCode", "" + data["Code"].ToString());
                setControlText("txtParamList", "" + data["Parameters"].ToString());
                setControlText("txtIncludes", "" + data["Includes"].ToString());
                setControlText("txtAssemblies", "" + data["Assemblies"].ToString());
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {

            bool readOnly = mode != ActivitySettingsPartMode.Edit;
            setControlReadOnly("txtActivityName", readOnly);
            setControlReadOnly("ddlActor", readOnly);
            setControlReadOnly("txtPropertyCount", readOnly);
            setControlReadOnly("ddlReturnType", readOnly);
            setControlReadOnly("tbTarget", readOnly);
            setControlReadOnly("ddlLanguage", readOnly);
            setControlReadOnly("tbCode", readOnly);
            setControlReadOnly("txtIncludes", readOnly);
            setControlReadOnly("txtAssemblies", readOnly);
            setControlVisible("btnAddParam", !readOnly);
            setControlVisible("btnRemoveParam", !readOnly);

            int count = 0;
            if (int.TryParse(getControlText("txtPropertyCount").ToString(), out count))
            {
                string paramList = "";

                for (int i = 1; i <= count; i++)
                {
                    try
                    {
                        paramList += getControlText("Parameter" + i) + ",";
                        setControlReadOnly("Parameter" + i.ToString(), readOnly);
                    }
                    catch { }
                }

                if (paramList.EndsWith(",")) paramList = paramList.Substring(0, paramList.Length - 1);

                setControlText("txtParamList", paramList);
            }

        }

        public override bool ValidateInputs()
        {
            bool result = true;
            return result;
        }

        public override string Title
        {
            get
            {
                string name = getControlText("txtActivityName");
                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
                return this.GetType().Name;
            }
        }

        #endregion

        #region FindControl override

        public override Control FindControl(string id)
        {
            // First, look at the immediate children.
            Control result = base.FindControl(id);
            if (result != null)
            {
                return result;
            }
            // Now search all descendants recursively.
            return findControl(this, id);
        }

        private Control findControl(Control parent, string id)
        {
            Control result = null;
            // Assume the parent has already been searched.
            foreach (Control child in parent.Controls)
            {
                // Search immediate children.
                result = child.FindControl(id);
                if (result != null)
                {
                    return result;
                }
                // Search all descendants recursively.
                result = findControl(child, id);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        #endregion

        #region System.Web.UI.Control members

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.InitializeControls();
        }

        #endregion

        #region Protected members

        protected Panel mainPanel = new Panel();

        protected virtual void InitializeControls()
        {
            try
            {
                this.Controls.Add(mainPanel);

                //mainPanel.BackImageUrl = GetPage().ClientScript.GetWebResourceUrl(this.GetType(),
                //    "Insight.FIM.Resources.Images.InsightWatermark.png");

                string path = "~/_CONTROLTEMPLATES/CustomCodeControl.ascx";
                this.Add(InitializeUserControl(path));
            }
            catch (Exception ex)
            {
                //log.Error("[InitializeControls]", ex);
                Literal lit = new Literal();
                lit.Text = ex.ToString();
                this.Controls.Add(lit);
            }
        }

        protected Control InitializeUserControl(string path)
        {
            Control c = null;
            System.Web.UI.Page page = GetPage();
            if (page != null)
            {
                c = page.LoadControl(path);
            }
            return c;
        }

        protected Page GetPage()
        {
            System.Web.UI.Page p = null;
            if (Page != null)
            {
                p = Page;
            }
            else if (this.Context != null)
            {
                p = (this.Context.Handler as Page);
            }
            return p;
        }

        protected virtual void Add(Control c)
        {
            mainPanel.Controls.Add(c);
        }

        #endregion

        #region WebUI utility methods

        protected void setControlReadOnly(string controlID, Boolean readOnly)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(TextBox))
                {
                    TextBox oText = (TextBox)target;
                    if (oText != null)
                    {
                        oText.ReadOnly = readOnly;
                    }
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(CheckBox))
                {
                    CheckBox oText = (CheckBox)target;
                    if (oText != null)
                    {
                        oText.Enabled = !readOnly;
                    }
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(RadioButtonList))
                {
                    RadioButtonList buttons = (RadioButtonList)target;
                    if (buttons != null)
                    {
                        buttons.Enabled = !readOnly;
                    }
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(DropDownList))
                {
                    DropDownList dropDownList = (DropDownList)target;
                    if (dropDownList != null)
                    {
                        dropDownList.Enabled = !readOnly;
                    }
                    return;
                }
            }
            catch { }
        }

        protected string getControlText(string controlID)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)target;
                    return textBox.Text;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(Label))
                {
                    Label label = (Label)target;
                    return label.Text;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(DropDownList))
                {
                    DropDownList dropDown = (DropDownList)target;
                    return dropDown.SelectedValue;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(RadioButtonList))
                {
                    RadioButtonList buttons = (RadioButtonList)target;
                    return buttons.SelectedValue;
                }
            }
            catch { }
            return "[Control '" + controlID + "' not found]";
        }

        protected void setControlText(string controlID, string value)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)target;
                    textBox.Text = value;
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(Label))
                {
                    Label label = (Label)target;
                    label.Text = value;
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(DropDownList))
                {
                    DropDownList dropDown = (DropDownList)target;
                    dropDown.SelectedValue = value;
                    return;
                }
            }
            catch { }
            try
            {
                if (target.GetType() == typeof(RadioButtonList))
                {
                    RadioButtonList buttons = (RadioButtonList)target;
                    buttons.SelectedValue = value;
                    return;
                }
            }
            catch { }
        }

        protected object getControlValue(string controlID)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(CheckBox))
                {
                    CheckBox textBox = (CheckBox)target;
                    return textBox.Checked;
                }
            }
            catch { }
            return "[Control '" + controlID + "' not found]";
        }

        protected void setControlValue(string controlID, object value)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(CheckBox))
                {
                    CheckBox textBox = (CheckBox)target;
                    bool enabled = textBox.Enabled;
                    textBox.Checked = (bool)value;
                    return;
                }
            }
            catch { }
        }

        protected int getControlIndex(string controlID)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(RadioButtonList))
                {
                    RadioButtonList buttons = (RadioButtonList)target;
                    return buttons.SelectedIndex;
                }
            }
            catch { }
            throw new ArgumentOutOfRangeException("Could not get control index of " + controlID);
        }

        protected void setControlIndex(string controlID, int value)
        {
            Control target = this.FindControl(controlID);
            try
            {
                if (target.GetType() == typeof(RadioButtonList))
                {
                    RadioButtonList buttons = (RadioButtonList)target;
                    buttons.SelectedIndex = value;
                    return;
                }
            }
            catch { }
        }

        protected bool getControlVisible(string controlID)
        {
            bool visible = true;
            Control target = this.FindControl(controlID);
            if (target != null)
            {
                visible = target.Visible;
            }
            return visible;
        }

        protected void setControlVisible(string controlID, Boolean visible)
        {
            Control target = this.FindControl(controlID);
            if (target != null)
            {
                target.Visible = visible;
            }
        }

        #endregion
    }
}
