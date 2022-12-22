namespace OVSdk
{
    public class CustomPresenter :
#if UNITY_EDITOR
    // Check for Unity Editor first since the editor also responds to the currently selected platform.
    CustomPresenterUnityEditor
#elif UNITY_ANDROID
    CustomPresenterAndroid
#elif UNITY_IPHONE || UNITY_IOS
    CustomPresenteriOs
#else
    CustomPresenterUnityEditor
#endif
    {
    }
}
