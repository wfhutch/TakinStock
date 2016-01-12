## TakinStock

This is a personal inventory app.  It is the culmination of three months of server-side progamming instruction at Nashville Software School.  The app was built using the .NET Framework with C#.

As of 1/11/2016 this app is deployed on Azure but throws an error when trying to display user items.  The app does run fine locally from Visual Studio.

#### User Stories

When user logs in they can either add items or search items.  User can also edit or delete items with buttons that will be on item display.

Use the .NET login.

Initial view should be a list of items owned that the user can scroll through.  Each item listing will have an Edit and Delete button.

If user wants to add a new item they can click Add Item link in navbar.

User can choose from a number of different categories to enter items into.
````
	1. Electronics
	2. Computers/Phones
	3. Musical Instruments
	4. Tools
	5. Jewelry
````
	
User will enter a description of the item.
````
	1. Make
	2. Description (e.g. tv, necklace, guitar, lawn mower)
	2. Model
	3. Serial Number
	4. Purchase Price
	5. Purchase Date
	6. Purchased From
	7. Boolean called Stolen with default setting of false.
	8. Boolean called Lost Due To Damage with default setting of false.
	9. Image
````
	
User can search by any of the description categories.

When user clicks on edit button they should be directed to same page as entering new item so that any field can be edited.

User can report an item as lost or stolen.  There should be checkboxes on edit page that change lost or stolen boolean to true.

User can delete an item if it was sold or otherwise disposed of.  User should not remove an item that was reported as lost or stolen.

Delete button should be included on each item listed.  Verification should happen if user clicks on delete button to confirm user does want to delete. ("This will permanently remove this item from the database.  Do you want to continue?" Select "yes" or "cancel")

User name and address should not be listed anywhere in database.  Ownership should be tied to email only.

Create report of stolen items as a PDF.  User could click on a report button and print all the items reported as either lost or stolen.

Initial view should show suggested stores in the user's area for replacement of items.  Will use Google Maps API based on geo location of browser to find nearby stores for each category of item.

#### Links

Wireframes - https://moqups.com/#!/edit/wfhutch/qU4eeQOm

