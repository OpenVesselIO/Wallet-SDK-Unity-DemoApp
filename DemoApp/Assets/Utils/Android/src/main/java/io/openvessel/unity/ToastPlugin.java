package io.openvessel.unity;

import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class ToastPlugin
{

    public static void showTextShort(final String text)
    {
        Toast.makeText( currentActivity, text, Toast.LENGTH_SHORT ).show();
    }

}
