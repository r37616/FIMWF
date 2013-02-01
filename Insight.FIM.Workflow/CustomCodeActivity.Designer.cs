using System.Workflow.Activities;

namespace Insight.FIM.Workflow.Activities
{
    public partial class CustomCodeActivity
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>x
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.resolveXPathActivity1 = new Ensynch.FIM.Workflow.Activities.ResolveXPathActivity();
            this.changeAttributeActivity1 = new Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity();
            this.ExecuteCode = new System.Workflow.Activities.CodeActivity();
            this.whileAllParametersNotResolved = new System.Workflow.Activities.WhileActivity();
            this.Preprocessing = new System.Workflow.Activities.CodeActivity();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            
            // 
            // resolveXPathActivity1
            // 
            activitybind5.Name = "CustomCodeActivity";
            activitybind5.Path = "ActorID";
            this.resolveXPathActivity1.Name = "resolveXPathActivity1";
            activitybind6.Name = "CustomCodeActivity";
            activitybind6.Path = "XPathResolved";
            activitybind7.Name = "CustomCodeActivity";
            activitybind7.Path = "TheActivityName";
            activitybind8.Name = "CustomCodeActivity";
            activitybind8.Path = "XPath";
            this.resolveXPathActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ResolveXPathActivity.ActorIDProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.resolveXPathActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ResolveXPathActivity.ThisActivityNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.resolveXPathActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ResolveXPathActivity.ResolvedValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.resolveXPathActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ResolveXPathActivity.XPathProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            
            // 
            // changeAttributeActivity1
            // 
            activitybind1.Name = "CustomCodeActivity";
            activitybind1.Path = "ActorID";
            activitybind2.Name = "CustomCodeActivity";
            activitybind2.Path = "Target";
            activitybind3.Name = "CustomCodeActivity";
            activitybind3.Path = "CodeReturnValue";
            this.changeAttributeActivity1.DiagnosticAfterActivityName = " (After)";
            this.changeAttributeActivity1.DiagnosticBeforeActivityName = " (Before)";
            this.changeAttributeActivity1.Name = "changeAttributeActivity1";
            activitybind4.Name = "CustomCodeActivity";
            activitybind4.Path = "TheActivityName";
            activitybind9.Name = "CustomCodeActivity";
            activitybind9.Path = "ReturnType";
            this.changeAttributeActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity.ActorIDProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.changeAttributeActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity.DestinationStringProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.changeAttributeActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity.DestinationValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.changeAttributeActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity.ThisActivityNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.changeAttributeActivity1.SetBinding(Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity.ValueTypeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // ExecuteCode
            // 
            this.ExecuteCode.Name = "ExecuteCode";
            this.ExecuteCode.ExecuteCode += new System.EventHandler(this._buildAndExecuteCode);
            // 
            // whileAllParametersNotResolved
            // 
            this.whileAllParametersNotResolved.Activities.Add(this.resolveXPathActivity1);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this._whileXPathNotResolved);
            this.whileAllParametersNotResolved.Condition = codecondition1;
            this.whileAllParametersNotResolved.Name = "whileAllParametersNotResolved";
            // 
            // Preprocessing
            // 
            this.Preprocessing.Name = "Preprocessing";
            this.Preprocessing.ExecuteCode += new System.EventHandler(this._preprocessing);
            // 
            // CustomCodeActivity
            // 
            this.Activities.Add(this.Preprocessing);
            this.Activities.Add(this.whileAllParametersNotResolved);
            this.Activities.Add(this.ExecuteCode);
            this.Activities.Add(this.changeAttributeActivity1);
            this.Name = "CustomCodeActivity";
            //this.TheActivityName = null;
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity Preprocessing;

        private Ensynch.FIM.Workflow.Activities.ResolveXPathActivity resolveXPathActivity1;

        private WhileActivity whileAllParametersNotResolved;

        private Ensynch.FIM.Workflow.Activities.ChangeAttributeActivity changeAttributeActivity1;

        private CodeActivity ExecuteCode;









































































    }
}
