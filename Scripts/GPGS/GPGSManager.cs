using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSManager
{
    private static GPGSManager instance = new GPGSManager();

    private GPGSManager() { }

    public static GPGSManager GetInstance()
    {
        return GPGSManager.instance;
    }

    public void Authenticate()
    {

        Debug.LogFormat("Authenticate");

        //인증 받기 
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);

        //다른방법 
        //PlayGamesPlatform.Activate();
        //Social.localUser.Authenticate((success)=> { });


    }

    public void ProcessAuthentication(SignInStatus status)
    {
        Debug.LogFormat("ProcessAuthentication: {0}", status);

        if (status == SignInStatus.Success)
        {
            //BaseType: UnityEngine.SocialPlatforms.Impl.UserProfile
            Debug.LogFormat("BaseType: {0}", Social.localUser.GetType().BaseType);

            var profile = Social.localUser as UnityEngine.SocialPlatforms.Impl.UserProfile;

            //                                 id | userName | isFriend | state
            //profile -----------------> 0 - Uninitialized - False -Offline
            /*
             * public override string ToString()
            {
            return id + " - " +
                userName + " - " +
                isFriend + " - " +
                state;
            }
            */

            //https://github.com/Unity-Technologies/UnityCsReference/blob/6ac92f7229c910266c744d6bb10b69f594404b5b/Modules/GameCenter/Managed/LocalService.cs#L59

            Debug.LogFormat("profile -----------------> {0}", profile);

            //Debug.LogFormat("authenticated: {0}", Social.localUser.authenticated);
            ////Debug.LogFormat("gameId: {0}", Social.localUser.gameId);
            //Debug.LogFormat("id: {0}", Social.localUser.id);
            //Debug.LogFormat("image: {0}", Social.localUser.image);   //url
            //Debug.LogFormat("state: {0}", Social.localUser.state);
            //Debug.LogFormat("userName: {0}", Social.localUser.userName);

            // Continue with Play Games Services
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

}
