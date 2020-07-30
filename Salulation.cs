using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace SalulationPlugin
{
    public class Salulation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.   
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName != "Create")
                return;

            // Obtain the organization service reference for web service calls.  
            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.  
                Entity entity = (Entity)context.InputParameters["Target"];

                //Verify that the target entity represents an entity type you are expecting
                //For exmaple: an account. If not the plu-in was not registered correctly
                if (entity.LogicalName != "contact")
                    return;

                try
                {
                    //read data from attributes
                    string firstName = entity.Attributes["firstname"].ToString();
                    string lastName = entity.Attributes["lastname"].ToString();
                    string gender = entity.Attributes["gendercode"].ToString();

                    //Check if the gender is empty
                    if (gender == null)
                    {
                        //assign data to attributes
                        entity.Attributes.Add("new_formalSalulation", "Dear Sir or Madam");
                        entity.Attributes.Add("new_informalSalulation", "Dear Sir or Madam");
                    }
                    else
                    {
                        //when the gender is not empty
                        //check if fist name and last name are empty
                        if (firstName == "" && lastName == "")
                        {
                            if (gender == "Female")
                            {
                                //assign data to attributes
                                entity.Attributes.Add("new_formalSalulation", "Dear Madam");
                                entity.Attributes.Add("new_informalSalulation", "Dear Madam");
                            }
                            else //gender is male
                            {
                                //assign data to attributes
                                entity.Attributes.Add("new_formalSalulation", "Dear Sir");
                                entity.Attributes.Add("new_informalSalulation", "Dear Sir");
                            }
                        }
                    }

                    //In case gender, first name and last name is not empty 
                    if (gender != null && firstName != "" && lastName != "")
                    {
                        if (gender == "Female")
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Ms " + lastName);
                            entity.Attributes.Add("new_informalSalulation", "Dear " + firstName);
                        }
                        else
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Mr " + lastName);
                            entity.Attributes.Add("new_informalSalulation", "Dear " + firstName);
                        }
                    }

                    //In case gender and last name are not empty
                    if (gender != null && firstName == "" && lastName != "")
                    {
                        if (gender == "Female")
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Ms " + lastName);
                            entity.Attributes.Add("new_informalSalulation", "Dear Madam");
                        }
                        else
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Mr " + lastName);
                            entity.Attributes.Add("new_informalSalulation", "Dear Sir");
                        }

                    }

                    //In case gender and first name are not empty
                    if (gender != null && firstName != "" && lastName == "")
                    {
                        if (gender == "Female")
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Madam");
                            entity.Attributes.Add("new_informalSalulation", "Dear " + firstName);
                        }
                        else
                        {
                            //assign data to attributes
                            entity.Attributes.Add("new_formalSalulation", "Dear Sir");
                            entity.Attributes.Add("new_informalSalulation", "Dear " + firstName);
                        }
                    }
                }

                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in MyPlug-in.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("MyPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
