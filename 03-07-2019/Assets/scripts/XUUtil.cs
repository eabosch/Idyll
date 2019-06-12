/*
An assortment of random useful functions/classes/stuff

Also includes some Debug developer settings

TODO convert some of these to extension methods!

*/
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

//TODO try and put camera stuff in here

public static class XUExtensions
{
    /// <summary>
    /// Call SetActive, if active != activeSelf
    /// </summary>
    /// <param name="gameObj"></param>
    /// <param name="active"></param>
    public static void SetActiveGentle(this GameObject gameObj, bool active)
    {
        if (gameObj.activeSelf != active)
        {
            gameObj.SetActive(active);
        }
    }

    public static float ManhattanMagnitude(this Vector3 v)
    {
        float ret = 0;
        for (int i = 0; i < 3; i++)
        {
            ret += Mathf.Abs(v[i]);
        }
        return ret;
    }
    public static float ManhattanMagnitude(this Vector2 v)
    {
        float ret = 0;
        for (int i = 0; i < 2; i++)
        {
            ret += Mathf.Abs(v[i]);
        }
        return ret;
    }

    public static V GetOrDefault<K, V>(this Dictionary<K, V> dict, K key, V optionalDefault = default(V))
    {
        return !dict.ContainsKey(key) ? optionalDefault : dict[key];
    }

    public static T GetOrDefault<T>(this Dictionary<string, object> dict, string key, T defaultVal = default(T))
    {
        if (dict == null || !dict.ContainsKey(key))
        {
            return defaultVal;
        }
        else
        {
            return (T)dict[key];
        }
    }

    public static void CopyToClipboard(this string s)
    {
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }


    public static void AppendAll(this StringBuilder sb, params object[] manyStrings)
    {
        for (int i = 0; i < manyStrings.Length; i++)
        {
            sb.Append(manyStrings[i]);
        }
    }

    public static List<int> FindAllIndicesOf(this string str, string value)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentException("the string to find may not be empty", "value");
        List<int> indexes = new List<int>();
        for (int index = 0; ; index += value.Length)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
                return indexes;
            indexes.Add(index);
        }
    }

    public static void TakeLocalValuesFrom(this Transform thiss, Transform other, bool includeScale = false)
    {
        thiss.localPosition = other.localPosition;
        thiss.localRotation = other.localRotation;
        if (includeScale)
        {
            thiss.transform.localScale = other.localScale;
        }
    }

    public static void TakeValuesFrom(this Transform thiss, Transform other, bool includeScale = false)
    {
        thiss.position = other.position;
        thiss.rotation = other.rotation;
        if (includeScale)
        {
            thiss.transform.localScale = other.localScale;
        }
    }

    public static void AddRange<T>(this ICollection<T> thiss, IEnumerable<T> toAdd)
    {
        foreach (T addd in toAdd)
        {
            thiss.Add(addd);
        }
    }

    public delegate void TFunction(float t);
    public delegate void ConditionalAction(bool success);

    public static Coroutine xuTween(this MonoBehaviour thiss, TFunction tfunc, float dur, float delay = 0)
    {
        return thiss.StartCoroutine(genericT(tfunc, dur, delay));
    }

    public static Coroutine xuTween(this MonoBehaviour thiss, TFunction tfunc1, float dur1, TFunction tfunc2, float dur2)
    {
        return thiss.StartCoroutine(genericT(tfunc1, dur1, tfunc2, dur2));
    }

    public static Coroutine xuDoWhenConditionMet(this MonoBehaviour thiss, System.Func<bool> condition, NoArgNoRetFunction action)
    {
        return thiss.StartCoroutine(xuDoWhenConditionMetRoutine( condition, action));
    }

    public static Coroutine xuDoWhenConditionMet(this MonoBehaviour thiss, System.Func<bool> condition, ConditionalAction action, float timeout)
    {
        return thiss.StartCoroutine(xuDoWhenConditionMetOrTimeoutRoutine(condition, action, timeout));
    }
    
    public static Quaternion AsQuaternion (this Vector4 thiss)
    {
        return new Quaternion(thiss.x, thiss.y, thiss.z, thiss.w);
    }
        
   

    public static Quaternion AxisTwist(this Quaternion thiss, Vector3 axis)
    {
        //Debug.Assert(Mathf.Approximately(axis.magnitude, 1));

        return XUUtil.GetAxisTwist(thiss, axis);
    }



    
    public static IEnumerator genericT(TFunction tfunc1, float dur1, TFunction tfunc2, float dur2)
    {
        float startTime = Time.time;
        while (Time.time < startTime + dur1)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / dur1);
            tfunc1(t);
            yield return new WaitForEndOfFrame();
        }
        //force call with 1
        tfunc1(1);

        startTime = Time.time;
        while (Time.time < startTime + dur2)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / dur2);
            tfunc2(t);
            yield return new WaitForEndOfFrame();
        }
        //force call with 1
        tfunc2(1);

    }

    public static IEnumerator genericT(TFunction tfunc, float dur, float delay)
    {
        yield return new WaitForSeconds(delay);

        //float startTime = Time.time;
        float counter = 0;
        while (counter < dur)
        {
            float t =  Mathf.Clamp01(counter / dur);
            tfunc(t);
            yield return new WaitForEndOfFrame();
            counter += Time.deltaTime;
        }
        //force call with 1
        tfunc(1);
    }

    static IEnumerator xuDoWhenConditionMetRoutine(System.Func<bool> condition, NoArgNoRetFunction action)
    {
        yield return new WaitUntil(condition);
        action();
    }

    static IEnumerator xuDoWhenConditionMetOrTimeoutRoutine(System.Func<bool> condition, ConditionalAction action, float timeOut)
    {
        float timeOutTime = Time.unscaledTime;
        yield return new WaitUntil( ()=>
                {
                    return condition() || Time.unscaledTime > timeOutTime;
                }
            );
        action(condition());
    }

    public static void ZeroLocalPosRot(this Transform thiss)
    {
        thiss.localPosition = Vector3.zero;
        thiss.localRotation = Quaternion.identity;
    }

    public delegate void NoArgNoRetFunction();

    public static void CopyTo<T>(this List<T> thiss, List<T> dest)
    {
        dest.Clear();
       for (int i = 0; i < thiss.Count; i++)
       {
            dest.Add(thiss[i]);
       }
    }

    public class EmptyEnumerable<T> : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EmptyEnumerator<T>();
        }
    }

    public class EmptyEnumerator<T> : IEnumerator<T>
    {
        public T Current
        {
            get
            {
                return default(T);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return null;
            }
        }

        public bool MoveNext()
        {
            return false;
        }
        public void Reset()
        {

        }

        public void Dispose()
        {
        }
    }



    public static Color withAlpha(this Color thiss, float a)
    {
         Color ret = thiss;
        ret.a = a;
        return ret;
    }

    public static Color withSaturation(this Color thiss, float sat)
    {
        Color ret = thiss;
        Color gray = new Color(ret.grayscale, ret.grayscale, ret.grayscale);
        return Color.LerpUnclamped(gray, ret, sat);
    }


    public static Vector3 withX(this Vector3 thiss, float x)
    {
        Vector3 ret = thiss;
        ret.x = x;
        return ret;   
    }
    public static Vector3 withY(this Vector3 thiss, float y)
    {
        Vector3 ret = thiss;
        ret.y = y;
        return ret;
    }



    public static Vector3 withZ(this Vector3 thiss, float z)
    {
        Vector3 ret = thiss;
        ret.z = z;
        return ret;
    }

    public static Vector3 Rearranged(this Vector3 thiss, int xIdx, int yIdx, int zIdx)
    {
        Vector3 ret = new Vector3(thiss[xIdx], thiss[yIdx], thiss[zIdx]);
        return ret;
    }



    public static Vector3 asXzVector3(this Vector2 thiss)
    {
        Vector3 ret = new Vector3(thiss.x, 0, thiss.y);
        return ret;
    }

    public static Vector3 asXyVector3(this Vector2 thiss)
    {
        Vector3 ret = new Vector3(thiss.x, thiss.y, 0);
        return ret;
    }

    public static Vector3 asVector3(this Vector4 thiss)
    {
        return new Vector4(thiss.x, thiss.y, thiss.z);
    }

    public static Vector4 asVector4(this Vector3 thiss, float w = 1)
    {
        return new Vector4(thiss.x, thiss.y, thiss.y, w);
    }

    public static void delayedFunction(this MonoBehaviour thiss, NoArgNoRetFunction func, float delay)
    {
        thiss.StartCoroutine(delayedFunctionRoutine(func, delay));
    } 

    static IEnumerator delayedFunctionRoutine (NoArgNoRetFunction func, float delay)
    {
        yield return new WaitForSeconds(delay);
        func();
    }

	//public static string toSti(this ResourceType grade)
	public static void SetLayerRecursively (this GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}
			
		obj.layer = newLayer;
			
		foreach (Transform child in obj.transform)
		{
			if (null == child)
			{
				continue;
			}
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}

    public static T GetComponentInParentOrSelf<T>(this MonoBehaviour thiss)
    {
        T ret = thiss.GetComponent<T>();
        ret = ret == null ? thiss.GetComponentInParent<T>() :ret;
        return ret;
    }

    public static T GetComponentInParentOrSelf<T>(this Transform thiss, int maxLevelsToTraverse = 1000)
    {
        T ret = thiss.GetComponent<T>();
        //ret = ret == null ? thiss.GetComponentInParent<T>() : ret;
        if (ret == null)
        {
            Transform nextToCheck = thiss.parent;
            int levelCounter = maxLevelsToTraverse;
            while(nextToCheck != null && levelCounter > 0)
            {
                ret = nextToCheck.GetComponent<T>();
                if (ret != null)
                {
                    break;
                }
                nextToCheck = nextToCheck.parent;
                levelCounter--;
            }
       
        }
        return ret;
    }


    public static bool EqualsAny<T>(this T thiss, T e1) where T : struct
    {
        T firstOne = thiss;// args[0];


        bool ret = thiss.Equals(e1);


        return ret;
    }


    public static bool EqualsAny<T>(this T thiss, T e1, T e2) where T : struct
    {
        T firstOne = thiss;// args[0];


        bool  ret = thiss.Equals(e1) || thiss.Equals(e2);

  
        return ret;
    }


    public static bool EqualsAny(this int thiss, int i1, int i2)
    {
        int firstOne = thiss;// args[0];


        bool ret = thiss == i1 || thiss == i2;
        return ret;
    }


    public static float CurrentStateTimeLeft(this Animator thiss, int layer = 0)
    {
        var stateInfo = thiss.GetCurrentAnimatorStateInfo(layer);
        return (1 - stateInfo.normalizedTime) * stateInfo.length;
    }

    public static float CurrentStateTimeSeconds(this Animator thiss, int layer = 0)
    {
        return thiss.GetCurrentAnimatorStateInfo(layer).normalizedTime * thiss.GetCurrentAnimatorStateInfo(layer).length;
    }

    public static float NextStateTimeSeconds(this Animator thiss, int layer = 0)
    {
        return thiss.GetNextAnimatorStateInfo(layer).normalizedTime * thiss.GetNextAnimatorStateInfo(layer).length;
    }

    public static string xuSubstring(this string thiss, int startIdx, int endIdx)
    {
        if (endIdx >= 0)
        {
            return thiss.Substring(startIdx, endIdx);
        }
        else
        {
            return thiss.Substring(startIdx, thiss.Length + endIdx);
        }
    }

}

public static class XUGizmos
{
    public static void DrawSphereCast(Vector3 start, float radius, Vector3 direction, float length)
    {
        //Gizmos.color = color;
    
        Gizmos.DrawWireSphere(start, radius);
        Gizmos.DrawWireSphere(start + (direction * length), radius);

        Quaternion[] rots = { Quaternion.Euler(-90,0,0), Quaternion.Euler(90, 0, 0), Quaternion.Euler(0, -90, 0), Quaternion.Euler(0, 90, 0) };
        foreach(Quaternion rot in rots)
        {
            Vector3 edgeLineOffset = rot * direction;
            edgeLineOffset *= radius;

            Gizmos.DrawLine(start + edgeLineOffset, start + direction * length + edgeLineOffset);

        }
    }
}

public class XUUtil 
{

    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public delegate T InterpolateFunction<T>(T a, T b, float t);

    public static Quaternion SmoothSampleQuaternionArray(Quaternion[] arr, float t)
    {
        float smoothIdx = t * (arr.Length - 1);
        int leftIdx = Mathf.FloorToInt(smoothIdx);
        int rightIdx = (int)Mathf.Min(leftIdx + 1, arr.Length - 1);//Mathf.CeilToInt(t * (arr.Length - 1));

        Quaternion leftElement = arr[leftIdx];
        Quaternion rightElement = arr[rightIdx];
        float mixAmt = smoothIdx - leftIdx;

        return Quaternion.Lerp(leftElement, rightElement, mixAmt);
    }

    public static T SmoothSampleArray<T>(T[] arr, float t, InterpolateFunction<T> interpolator) 
    {
        float smoothIdx = t * (arr.Length - 1);
        int leftIdx  = Mathf.FloorToInt(smoothIdx);
        int rightIdx = (int) Mathf.Min(leftIdx + 1, arr.Length - 1);//Mathf.CeilToInt(t * (arr.Length - 1));

        T leftElement = arr[leftIdx];
        T rightElement = arr[rightIdx];
        float mixAmt = smoothIdx - leftIdx;

        return interpolator(leftElement, rightElement, mixAmt);
    }

    public static Vector3 SmoothSampleArray(Vector3[] arr, float t)
    {
        return SmoothSampleArray(arr, t, Vector3.Lerp);
    }

    public static Quaternion SmoothSampleArray(Quaternion[] arr, float t)
    {
        return SmoothSampleArray(arr, t, RobustLerp);
    }

    static Quaternion RobustLerp(Quaternion a, Quaternion b, float t)
    {
        
        Quaternion ret = Quaternion.Lerp(a, b, t);
        
        for (int i = 0; i < 4; i++)
        {
            if (ret[i] != ret[i])
            {
                return Quaternion.identity;
            }
        }
        return ret;
    }

    public static Vector4 SmoothSampleArray(Vector4[] arr, float t)
    {
        return SmoothSampleArray(arr, t, Vector4.Lerp);
    }

  

    public static void DrawCenteredText(Vector2 offset, string text, int fontSize, Color fontColor, string font)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = fontColor;
        style.fontSize = fontSize;
        style.font = (Font)Resources.Load("Fonts/" + font);
        Vector2 size = style.CalcSize(new GUIContent(text));
        Vector2 position = new Vector2(Screen.width, Screen.height) / 2 - size / 2;
        position += offset;
        GUI.Label(new Rect(position.x, position.y, size.x, size.y), text, style);
    }

    public static void DrawText(Vector2 position, string text, int fontSize, Color fontColor, string font = null)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = fontColor;
        style.fontSize = fontSize;

        if (font != null)
        {
            style.font = (Font)Resources.Load("Fonts/" + font);
        }
        
        Vector2 size = style.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(position.x, position.y, size.x, size.y), text, style);
    }



    public static void DrawTextAtWorldPosition(Vector3 worldPt, string text, int fontSize, Color fontColor, Camera cam, Vector2 offset = new Vector2())
    {
        if (cam == null)
        {
            return;
        }
        Vector3 position = cam.WorldToScreenPoint(worldPt);//Camera.main.WorldToScreenPoint(worldPt);
        position.y = Screen.height - position.y;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = fontColor;
        style.fontSize = fontSize;
        Vector2 size = style.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(position.x + offset.x - size.x/2, position.y - offset.y - size.y/2, size.x, size.y), text, style);
        //GUI.Label(new Rect(position.x, position.y, size.x, size.y), text, style);
    }

    /// <summary>
    ///  is val >= min, and < max?
    /// </summary>
    /// <param name="val"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool NumberIsBetween(float val, float min, float max)
    {
        return val >= min && val < max;
    }

    /// <summary>
    ///  true if val greater than or equal min, and strictly less than max
    /// </summary>
    /// <param name="val"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool NumberIsBetween(int val, int min, int max)
    {
        return val >= min && val < max;
    }



    public static Vector2 FloatyPerlinVector(float t)
    {
        float t1 = 1.007f * t;
        float t2 = 1f * t;
        float t3 = 0.9973f * t;
        float t4 = 0.9913f * t;
        float p1 = Mathf.PerlinNoise(t1, t3);
        float p2 = Mathf.PerlinNoise(t4, t2);
        return new Vector2(p1, p2);
    }


    /*public class StoredTransform
	{
		public Vector3 position;
		public Quaternion rotation;


        public Vector3 forward
        {
            get { return rotation * Vector3.forward; }
            set { rotation = Quaternion.LookRotation(value); }
        }

   
		
		public StoredTransform (Transform t)
		{
			this.position = t.position;
			this.rotation = t.rotation;
		}

		public static implicit operator UnityEngine.Transform(StoredTransform t)  // implicit digit to byte conversion operator
		{
			System.Console.WriteLine("conversion occurred");
			return new StoredTransform(t);
		}
	}*/


    public static void SwingTwistDecomposition(Quaternion rotation, Vector3 twistAxis, out Quaternion swing, out Quaternion twist)
    {
        twist = GetAxisTwist(rotation, twistAxis);
        swing = rotation * Quaternion.Inverse(twist);
    }

    public static Quaternion GetAxisTwist(Quaternion rotation, Vector3 twistAxis)
    {
        //Courtesy of, compliments to this person:
        // http://www.euclideanspace.com/maths/geometry/rotations/for/decomposition/forum.htm
        Vector3 rotation_axis = new Vector3(rotation.x,
        rotation.y, rotation.z);
        // return projection v1 on to v2 (parallel component)
        // here can be optimized if default_dir is unit
        Vector3 proj = Vector3.Project(rotation_axis, twistAxis);
        Vector4 normed = new Vector4(proj.x, proj.y, proj.z, rotation.w).normalized;
        Quaternion twist = new Quaternion(normed.x, normed.y, normed.z, normed.w);
        //twist_rotation.normalize();
        return twist;

        /*# ifdef _DEBUG
            xxquaternion composite = dir_rotation * twist_rotation;
            composite -= orientation;
        ASSERT(composite.magnitude() < 0.00001f );
        #endif //_DEBUG */
    }


    public static void EnforceComponentReferenceType<T>(ref Component toAssign) where T : class
    {
        Component candidate = toAssign;

        if (toAssign != null)
        {
            toAssign = (candidate as T) as Component;
        }

        if (toAssign == null && candidate != null)// && toAssign.gameObject != candidate.gameObject)
        {
            T intermediate = candidate.GetComponent<T>();
            toAssign = intermediate != null ? intermediate as Component : null;
        }
    }

    static float Frac(float input)
    {
        return input - Mathf.Floor(input);
    }

    static float TCShaderHash(float input)
    {
        float ret = Mathf.Sin(input) * 43758.5453f;
        return ret - Mathf.Floor(ret);
    }

    public static float TCShaderNoise(Vector3 input)
    {
        Vector3 p = new Vector3(Mathf.Floor(input.x), Mathf.Floor(input.y), Mathf.Floor(input.z));
        Vector3 f = new Vector3(input.x - p.x, input.y - p.y , input.z - p.z);
        f = Vector3.Scale(Vector3.Scale(f, f), (Vector3.one * 3f - 2f * f));
        float n = p.x + p.y * 57.0f + 113.0f * p.z;
        return Mathf.LerpUnclamped(Mathf.LerpUnclamped(Mathf.LerpUnclamped(TCShaderHash(n + 0f), TCShaderHash(n + 1f), f.x),
                          Mathf.LerpUnclamped(TCShaderHash(n + 57.0f), TCShaderHash(n + 58f), f.x), f.y),
                          Mathf.LerpUnclamped(Mathf.LerpUnclamped(TCShaderHash(n + 113f), TCShaderHash(n + 114f), f.x),
                          Mathf.LerpUnclamped(TCShaderHash(n + 170.0f), TCShaderHash(n + 171.0f), f.x), f.y), f.z);
    }

    public static float TCShaderRand(float input0, float input1)
    {
        return Frac(Mathf.Sin(Vector2.Dot(new Vector2(input0, input1), new Vector2(12.9898f, 78.233f))) * 43758.5453f);
    }

#if UNITY_EDITOR
    public static void DoToAllCompononentsInAllScenes<T>(System.Action<T> func)
    {
        for (int i = 0; i < UnityEditor.SceneManagement.EditorSceneManager.sceneCount; i++)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);
            if (!scene.isLoaded)
            {
                continue;
            }
            foreach (GameObject rootOb in scene.GetRootGameObjects())
            {
                foreach (T t in rootOb.GetComponentsInChildren<T>())
                {
                    func(t);
                }
            }
        }
    }


    static void DoToAllGameObjectsInAllScenesHelper(GameObject rootOb, System.Action<GameObject> func)
    {
        func(rootOb);
        foreach (Transform t in rootOb.transform)
        {
            DoToAllGameObjectsInAllScenesHelper(t.gameObject, func);
        }

    }
    public static void DoToAllGameObjectsInAllScenes(System.Action<GameObject> func)
    {
        for (int i = 0; i < UnityEditor.SceneManagement.EditorSceneManager.sceneCount; i++)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);
            if (!scene.isLoaded)
            {
                continue;
            }
            foreach (GameObject rootOb in scene.GetRootGameObjects())
            {
                DoToAllGameObjectsInAllScenesHelper(rootOb, func);
            }
        }
    }

#endif

}
/*
public static class XuOneTimeDebugMessage
{
    #if UNITY_EDITOR
    static Dictionary<Object, List<string>> _firedMessages = new Dictionary<Object, List<string>>();
    #endif

    public static void PrintOnce(Object originator, string message)
    {
        #if UNITY_EDITOR
        bool dictContainsObject = _firedMessages.ContainsKey(originator);
        if (!dictContainsObject ||  (!_firedMessages[originator].Contains(message)))
        {
            Debug.LogError(message, originator);  
            if (!dictContainsObject)
            {
                _firedMessages[originator] = new List<string>(32);
            }
            _firedMessages[originator].Add(message);
        }
        #endif
    }

}
*/