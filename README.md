# Creating custom salutations on the standard contact entity of D365 with a plugin in c#

Creating new fields on the standart contact entity of D365 with the help of a plugin in c# 

Business Requeriment: The customer needs an automatic process building a custom salutation when creating or updating a contact
(standard contact entity) in D365. 

The following shall be implemented:

• In addition to the standard salutation field, two custom salutation fields shall be created and placed on
the standard contact form (Display name: “formal salutation” and “informal salutation”
• If a contact is created by a user the new fields shall be filled automatically according the predefined
rules
• The user shall be able to adjust the values of both salutation fields at any time
• If the name of the contact changes the name in the salutation fields shall be updated accordingly
• If there is additional information to the contact (e.g. gender is set up) any eventual standard salutation
shall be changed to the specific value. Salutations adjusted by the user shall remain the same
