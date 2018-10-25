# SxaGlobalFieldValidator
an SXA module to validate fields at a site level rather than at the template field level

By default validation rules are applied on the template field item under the template.  This means that every other template that inherits yours will automatically get the validation rules applied to it.

My solution is to pull the validation rule definitions optionally out of the template field and into a global library of items that contain a template to template field mapping.

## To install
### Recommended
1. Clone this repo
1. Copy the project to your website's project
1. Change the namespaces to match your website
1. Add publishing capabilities in your perferred method
1. Install the Sitecore package found in the root of the repo
1. Track the items found in the package with your favorite filesystem serialization tool (Unicorn/TDS)
/sitecore/system/Settings/Foundation/Experience Accelerator/Content Validation/Content Validation Site Setup
/sitecore/templates/Foundation/Experience Accelerator/Content Validation/Global Field Rule
/sitecore/templates/Foundation/Experience Accelerator/Content Validation/Global Field Rule Folder

Then install the "Content Validation" module on your site and under settings you will find an item for "Global Field Rules" where you can build your field validation to template mapping.

### Alternative
1. Install Sitecore package found in the root of the repo

Then install the "Content Validation" module on your site and under settings you will find an item for "Global Field Rules" where you can build your field validation to template mapping.
