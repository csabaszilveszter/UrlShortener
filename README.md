# UrlShortener

A url shortener api.

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/csabaszilveszter/UrlShortener)

If you already have VS Code and Docker installed, you can click the badge above or [here](https://vscode.dev/redirect?url=vscode://ms-vscode-remote.remote-containers/cloneInVolume?url=https://github.com/csabaszilveszter/UrlShortener) to get started. Clicking these links will cause VS Code to automatically install the Dev Containers extension if needed, clone the source code into a container volume, and spin up a dev container for use.

Run a database update before trying to execute the application:

```sh
cd UrlShortener/UrlShortener.Api
dotnet ef database update
```

Then, when you run the project in VS Code your browser should navigate to <https://username-random-name-5000.preview.app.github.dev/swagger/index.html> (as a pop-up if using GitHub Codespaces in the browser) or <http://127.0.0.1:5000/swagger/index.html> (when on local devcontainers).
