# kontent-mvc-navigation
> :warning: This README is under construction. :warning:

This application serves as a supporting repository for a blog post series showing 
how to build a dynamic navigation menu using .NET Core MVC and the 
[Kentico Kontent headless CMS](https://kontent.ai/). The project is broken up 
across different branches ("Parts") to showcase code changes throughout
the different blog posts.

> :newspaper: links to the related blog post will be included when published.

## Application setup

### Running the application
To run the app:
1. Clone the app repository with your favorite GIT client
   1. For instance, you can use [Visual Studio](https://www.visualstudio.com/vs/), [Visual Studio Code](https://code.visualstudio.com/), [GitHub Desktop](https://desktop.github.com/), etc.
   1. Alternatively, you can download the repo as a ZIP file, however, this will not adapt line endings in downloaded files to your platform (Windows, Unix).
1. Open the solution in Visual Studio.

### Importing the blog post project
To import the sample project:

1. Go to app.kontent.ai and [create an empty project](https://docs.kontent.ai/tutorials/manage-kontent/projects/manage-projects#a-creating-projects)

1. Go to _Project Settings_ > _Localization_
    - Edit "Default Language" to be:
        - **Language name:** English (United States)
        - **Codename:** en-US
    - Create new language:
        - **Language name:** Spanish (Spain)
        - **Codename:** es-ES

1. Go to _Project Settings_ > _API Keys_, note the _Project ID_ and _Management API_ keys for later use

1. Install the [Kontent Backup Manager](https://github.com/Kentico/kontent-backup-manager-js) and import data to newly created project from the [part_four_backup.zip](https://github.com/kentico-michaelb/kontent-mvc-navigation/blob/PartFour/part_four_backup.zip) file (place appropriate values for apiKey and projectId arguments):

    ```sh
    npm i -g @kentico/kontent-backup-manager

    kbm --action=restore --apiKey=<Management API key> --projectId=<Project ID> --zipFilename=part_three_backup
    ```

    > Alternatively, you can use the [Template Manager UI](https://github.com/Kentico/kontent-template-manager) for importing the content. <br/>
    > **Note:** All language variants in the Kentico Kontent project must be published. The Kontent Backup Manager does this by default, and there is a "Publish language variants after import" option in the Kontent Template Manager UI.

### Connecting to the sample project
Perform the following steps to connect your MVC application to the imported project:

1. In Kentico Kontent, choose _Project settings_ from the app menu.
1. Under _Production Environment Settings_, choose _API keys_ and copy the *Project ID*.
1. Open the `appsettings.json` file.
1. Use the values from your Kentico Kontent project in the `appsettings.json` file:

    * **Project ID**: Insert your project ID into the `ProjectId` application setting.

```json
    {
    "DeliveryOptions": {
      "ProjectId": "<your Kontent project ID>"
    },
  }
```

## Web Spotlight
> **Note**: The showcased navigation can be setup without Web Spotlight by manually creating the [Homepage](https://docs.kontent.ai/tutorials/manage-kontent/projects/set-up-web-spotlight#a-homepage-in-web-spotlight) and [Page](https://docs.kontent.ai/tutorials/manage-kontent/projects/set-up-web-spotlight#a-page-in-web-spotlight) content types.

Web Spotlight is used throughout the blog series and dictates the structure of the navigation menu.  It is an additional (optional) tool for Kentico Kontent focused on website management. For this project, it allows for:

*   Seeing the hierarchy of the navigation in a page tree 
*   Creating new pages from the page tree
*   Previewing changes in the Kontent UI

Web Spotlight is a paid feature and must be activated by a member of the Kentico Kontent Sales team for your subscription before it can be used. More information about Web Spotlight and activation can be seen in the [official Kentico Kontent documentation](https://docs.kontent.ai/tutorials/set-up-kontent/set-up-your-project/web-spotlight "Web Spotlight documentation").

Web Spotlight uses Kentico Kontent's "Preview" functionality in order to show the live view of the site within the UI.

**To set up preview URLs for your project:**

1.  In Kentico Kontent, choose  Project settings   from the app menu.
2.  Under Environment settings, choose Preview URLs.
3.  Type in the preview URLs for each type of preview-able content.

More details about setting up preview and Web Spotlight can be seen in the [official Kentico Kontent documentation.](https://docs.kontent.ai/tutorials/develop-apps/build-strong-foundation/set-up-preview "Kontent Documentation - set up preview for content items")

> **Note:** Preview URLs require an `https://` protocol and a URL accessible to Kontent. Without a valid SSL certificate, Kontent responds with secure connection errors. When developing apps locally, see how to [serve pages over HTTPS](https://create-react-app.dev/docs/using-https-in-development/) in combination with [ngrok](https://ngrok.com/docs)'s forwarded address.

## Localization
This application uses route localization and tag helpers provided by the [AspNetCore.Mvc.Routing.Localization](https://github.com/tomasjurasek/AspNetCore.Mvc.Routing.Localization) package.