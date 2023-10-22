// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = 
{
  production: false,
  api:"http://localhost:5217/api",
  vidGuardApiKey:'nAe2qQ3XpwyPKN4OwqXxDLjbdJrWVMa67oz',
  vidGuardUpload:'https://api.vidguard.to/v1/upload/server?key=',
  vidGuardPoster:' https://api.vidguard.to/v1/poster/upload',
  vidGuardClone:' https://api.vidguard.to/v1/video/clone?key=',
  videoURL:'https://vid-guard.com/e/',
  firebase:
  {
    projectId: "moviepiratedweb",
    apiKey: "AIzaSyAC0mtG3IqRYUZcnKsyj_A1d3Qsi9NOY-s",
    clientEmail: "firebase-adminsdk-epkfz@moviepiratedweb.iam.gserviceaccount.com",
    storageBucket:'gs://moviepiratedweb.appspot.com'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
