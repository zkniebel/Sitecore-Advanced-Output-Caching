# Sitecore Rules-Based Output Caching
This solution contains a helix-compliant Sitecore module (demo project included) with a rules-based output caching implementation that allows you to configure custom rules to control your rendering caching variance.

**This solution can give you the power to cache personalized Sitecore renderings with minimal effort!**

## How it Works
The module adds a new "Vary by Rule" droptree field to the "Caching" layout section template. If caching is enabled, the rendering is marked as "cacheable", and a rule folder has been selected from this field then the module will automatically run the rules in the selected folder to manipulate the cache key for the rendering. A base class, `BaseCacheVarianceRuleAction<TRuleAction>` is included with the module to make writing your own custom cache variance rule actions as simple as implementing two methods: `GetKey` and `GetValue`. 

## Performance Recommendations
You can make your rules as robust or as lean as you want. In order to maximize the performance value that this module can add to your solution (and to avoid the performance hit that could be incurred if this module is abused), the following recommendations should be adhered to when writing your rules:

- Pass values to your rules through rule macros rather than reading off of items whenever you can
- Avoid excessively retrieving items
- Keep the number of rules and rule actions that you use as small as possible
- Don't use Content Search, Sitecore Query or Fast Query in any rules
- Don't use any `Item.Axes` methods in any of your rules 

## Installation
In order to set up the module locally:

Install a local instance of Sitecore 9.1 and open up this solution in Visual Studio.
Ensure that you have TDS Classic installed on your machine and install it if you do not
Create a TdsGlobal.config.user file in your solution root and set the values to match your local instance
Install the TDS-Sitecore Connector (right-click on a TDS project, then click "Install Sitecore Connector")
Build the project
Once you have completed the setup, browse to the front-end of your local instance in your web browser and you should see an informative rendering there that repeats the installation instructions and demo instructions.

## How the Demo Works
To demonstrate the functionality of the rules-based output caching implementation, the demo includes two parts:

- Renderings (this rendering, to be precise)
- Custom rule action to control output cache variance

The rendering renders out two things: the "Title" field value and the "Text" field value of the context item. In the custom rule action, the "Title" field is set to be used as the cache variance key part, meaning that the rendering's cache state will vary by the value of the "Title" field.

**IMPORTANT:** In order to prevent the `HtmlCacheClearer` from running and clearing out the cache every time we change and publish the item, a config patch is included with the demo to make the "website" site use the "master" database instead of the "web" database. It just didn't feel right to disable the `HtmlCacheClearer` for a demo of an output caching module, so I decided to skip the publishing altogether. This should not be used on production; this is for demo purposes only, so that you can test the caching function more easily.

## Using the Demo
In order to use this demo, try the below.

### Testing the Cache Rule is Working
Perform the following steps without publishing:

1. Request the homepage in the browser
2. Log into Sitecore and make a change that you will be able to see on the front-end to the value of both the "Title" and "Text" fields on the home item, and save.
3. Navigate back to the front-end of the site and notice that both values have changed
4. Go back into Sitecore and make another noticeable change to just the "Text" field
5. Navigate back to the front-end of the site and notice that neither value will appear to have changed
6. Go back into Sitecore and make another noticeable change to just the "Title" field
7. Navigate back to the front-end of the site and notice that both values will have changed

### Testing `HtmlCacheClearing`
Repeat the same steps as before but publish your changes after step 4. You will notice that the updated version displays on the page, even though the context database is still "master". This is because when you publish, the `HtmlCacheClearer` runs and wipes out everything in your Html Output Cache. When you see the newly set value appear this means that cache clearing on publish is working properly.
