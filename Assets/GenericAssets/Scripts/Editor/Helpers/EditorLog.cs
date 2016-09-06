using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// =================================================================================================================
public class EditorLog
{
    // -------------------------------------------------------------------------------------------------------------
    private int mMaxLines;
    private List<string> mLines;
    
    private bool mUpdate;
    private StringBuilder mStringBuilder;
    private string mString;
    
    private Vector2 mScrollView;
    
    private int mWidth;
    private GUILayoutOption mWidthOption;
    private int mHeight;
    private GUILayoutOption mHeightOption;
    
    // -------------------------------------------------------------------------------------------------------------
    public EditorLog(int maxlines, int width, int height)
    {
        mMaxLines = maxlines;
        mLines = new List<string>(mMaxLines + 1);
        
        mUpdate = true;
        mStringBuilder = new StringBuilder();
        mString = string.Empty;
        
        mScrollView = Vector2.zero;
        
        mWidth = width;
        mHeight = height;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void Add(string line)
    {
        while ( mLines.Count > mMaxLines )
        {
            mLines.RemoveAt(0);
        }
        
        mLines.Add(line);
        mUpdate = true;
        mScrollView = Vector2.up * 999999.0f;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void Clear()
    {
        mLines.Clear();
        mUpdate = true;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void GUILogBox()
    {
        if ( mWidthOption == null )
        {
            mWidthOption = GUILayout.Width(mWidth);
        }
        
        if ( mHeightOption == null )
        {
            mHeightOption = GUILayout.Height(mHeight);
        }
        
        if ( mUpdate )
        {
            mStringBuilder.Length = 0;
            
            foreach ( string s in mLines )
            {
                mStringBuilder.Append(s).Append(System.Environment.NewLine);
            }
            
            mString = mStringBuilder.ToString();
            
            mUpdate = false;
        }
        
        if ( mWidth != 0 && mHeight != 0 )
        {
            GUILayout.BeginVertical(GUI.skin.box, mWidthOption, mHeightOption);
        }
        else if ( mWidth != 0 )
        {
            GUILayout.BeginVertical(GUI.skin.box, mWidthOption);
        }
        else if ( mHeight != 0 )
        {
            GUILayout.BeginVertical(GUI.skin.box, mHeightOption);
        }
        else
        {
            GUILayout.BeginVertical(GUI.skin.box);
        }
        
        {
            EditorGUIControls.Title("Log");
        
            mScrollView = GUILayout.BeginScrollView(mScrollView, false, true);
            GUILayout.Label(mString);
            GUILayout.EndScrollView();
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button("Clear", EditorGUIControls.BUTTON_WIDTH) )
                {
                    Clear();
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
    
    /*
    private void GUILogBox()
    {
            
            while ( mLog.Count > LOG_MAX_LINES )
            {
                mLog.RemoveAt(0);
            }
            
            mLogBuilder.Length = 0;
            foreach ( string log in mLog )
            {
                mLogBuilder.Append(log).Append(System.Environment.NewLine);
            }
            
            mLogScrollViewPosition = GUILayout.BeginScrollView(mLogScrollViewPosition, false, true);
            GUILayout.Label(mLogBuilder.ToString());
            GUILayout.EndScrollView();
            
            GUILayout.BeginHorizontal();
            {
                //GUILayout.Label(mLogScrollViewPosition.ToString());
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button("Clear", GUILayout.Width(BUTTON_WIDTH) ) )
                {
                    mLog.Clear();
                }
            }
            GUILayout.EndHorizontal();
    }
    */
}
