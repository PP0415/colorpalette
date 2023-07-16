using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewMonoBehaviour : MonoBehaviour
{
    [SerializeField] Slider R_Slider;
    [SerializeField] Slider G_Slider;
    [SerializeField] Slider B_Slider;
    [SerializeField] Slider H_Slider;
    [SerializeField] TMP_InputField R_inputField;
    [SerializeField] TMP_InputField G_inputField;
    [SerializeField] TMP_InputField B_inputField;
    [SerializeField] TextMeshProUGUI R_text;
    [SerializeField] TextMeshProUGUI G_text;
    [SerializeField] TextMeshProUGUI B_text;
    [SerializeField] UnityEngine.UI.Image ColorImage;
    [SerializeField] UnityEngine.UI.Image CanvasImage;
    [SerializeField] UnityEngine.UI.Image H_Canvas;
    [SerializeField] UnityEngine.UI.Image CanvasMakerImage;
    [SerializeField] RectTransform CanvasMakerTransform;
    [SerializeField] RectTransform CanvasTransform;
    float R;
    float G;
    float B;
    float H;
    float S;
    float V;
    private void Start()
    {
        R_Slider.value = 0;
        G_Slider.value = 0;
        B_Slider.value = 0;
        R_inputField.text = "0";
        G_inputField.text = "0";
        B_inputField.text = "0";
        R = 0;
        G = 0;
        B = 0;
        ColorImage.color = new Color(R / 255, G / 255, B / 255, 1);
        RGBToHSV();
        CanvasSetting();
        Texture2D H_texture = new Texture2D(360, 1);
        H_Canvas.sprite = Sprite.Create(H_texture, new Rect(0, 0, 360, 1), new Vector2(0.5f, 0.5f));
        for (int y = 0; y < H_texture.height; y++)
        {
            for (int x = 0; x <= H_texture.width; x++)
            {
                S = 100;
                V = 100;
                H = x;
                HSVToRGB();
                Color H_Canvascolor = new Color(R / 255, G / 255, B / 255);
                H_texture.SetPixel(x, y, H_Canvascolor);
            }
        }
        H_texture.Apply();
        R = 0;
        G = 0;
        B = 0;
        RGBToHSV();
    }

    private void RGBToHSV()
    {
        R = (int)R;
        G = (int)G;
        B = (int)B;
        float rgbmax = Mathf.Max(R, G, B);
        float rgbmin = Mathf.Min(R, G, B);
        var rgbd = rgbmax - rgbmin;
        if (rgbd == 0)
        {

        }
        else if (rgbmax == R)
        {
            H = 60 * ((G - B) / rgbd);
        }
        else if (rgbmax == G)
        {
            H = 60 * ((B - R) / rgbd + 2);
        }
        else if (rgbmax == B)
        {
            H = 60 * ((R - G) / rgbd + 4);
        }

        if (H < 0)
        {
            H = H + 360;
        }
        S = rgbmax == 0 ? S : (rgbd / rgbmax) * 100;
        V = rgbmax * 100 / 255;
        H = (int)H;
        S = (int)S;
        V = (int)V;
    }

    private void HSVToRGB()
    {
        H = (int)H;
        S = (int)S;
        V = (int)V;
        float hsvmax = V * 255 / 100;
        float hsvmin = hsvmax - (S / 100 * hsvmax);
        if ((H >= 0 && H <= 60))
        {
            R = hsvmax;
            G = (H / 60) * (hsvmax - hsvmin) + hsvmin;
            B = hsvmin;
        }
        else if (H >= 60 && H <= 120)
        {
            R = ((120 - H) / 60) * (hsvmax - hsvmin) + hsvmin;
            G = hsvmax;
            B = hsvmin;
        }
        else if (H >= 120 && H <= 180)
        {
            R = hsvmin;
            G = hsvmax;
            B = ((H - 120) / 60) * (hsvmax - hsvmin) + hsvmin;
        }
        else if (H >= 180 && H <= 240)
        {
            R = hsvmin;
            G = ((240 - H) / 60) * (hsvmax - hsvmin) + hsvmin;
            B = hsvmax;
        }
        else if (H >= 240 && H <= 300)
        {
            R = ((H - 240) / 60) * (hsvmax - hsvmin) + hsvmin;
            G = hsvmin;
            B = hsvmax;
        }
        else if (H >= 300 && H <= 360)
        {
            R = hsvmax;
            G = hsvmin;
            B = ((360 - H) / 60) * (hsvmax - hsvmin) + hsvmin;
        }
        R = (int)R;
        G = (int)G;
        B = (int)B;
    }

    public void ColorDropper()
    {
        CanvasMakerTransform.position = Input.mousePosition;
        var x = CanvasMakerTransform.anchoredPosition.x; var y = CanvasMakerTransform.anchoredPosition.y;
        x = Mathf.Clamp(x, 0, CanvasTransform.rect.max.x * 2); y = Mathf.Clamp(y, 0, CanvasTransform.rect.max.y * 2);
        var tmpX = x; var tmpY = y;
        x = (int)((x / (CanvasTransform.rect.max.x * 2)) * 100); y = (int)((y / (CanvasTransform.rect.max.y * 2)) * 100);
        CanvasMakerTransform.anchoredPosition = new Vector2((int)tmpX, (int)tmpY);
        S = x; V = y;
        HSVToRGB();
        R_inputField.SetTextWithoutNotify(R.ToString());
        R_text.text = R_inputField.text;
        R_Slider.SetValueWithoutNotify(R);
        G_inputField.SetTextWithoutNotify(G.ToString());
        G_text.text = G_inputField.text;
        G_Slider.SetValueWithoutNotify(G);
        B_inputField.SetTextWithoutNotify(B.ToString());
        B_text.text = B_inputField.text;
        B_Slider.SetValueWithoutNotify(B);
        ColorImage.color = new Color(R / 255, G / 255, B / 255);
        CanvasMakerImage.color = new Color(1 - R / 255, 1 - G / 255, 1 - B / 255);
    }
    public void CanvasSetting()
    {
        float tmpR = R;
        float tmpG = G;
        float tmpB = B;

        Texture2D texture = new Texture2D(100, 100);
        CanvasImage.sprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                S = x;
                V = y;
                HSVToRGB();
                Color Canvascolor = new Color(R / 255, G / 255, B / 255);
                texture.SetPixel(x, y, Canvascolor);
            }
        }
        texture.Apply();
        R = tmpR;
        G = tmpG;
        B = tmpB;
        RGBToHSV();
    }

    public void CanvasMakerSetting()
    {
        RGBToHSV();
        CanvasMakerImage.color = new Color(1 - R / 255, 1 - G / 255, 1 - B / 255);
        CanvasMakerTransform.anchoredPosition = new Vector2((S * (CanvasTransform.rect.max.x * 2)) / 100, (V * (CanvasTransform.rect.max.x * 2)) / 100);
    }

    public void R_SliderChange()
    {
        R = R_Slider.value;
        R_inputField.SetTextWithoutNotify(R.ToString());
        R_text.text = R_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }

    public void G_SliderChange()
    {
        G = G_Slider.value;
        G_inputField.SetTextWithoutNotify(G.ToString());
        G_text.text = G_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }

    public void B_SliderChange()
    {
        B = B_Slider.value;
        B_inputField.SetTextWithoutNotify(B.ToString());
        B_text.text = B_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }

    public void H_SliderChange()
    {
        var tmpS = S; var tmpV = V;
        H = H_Slider.value;
        HSVToRGB();
        R_inputField.SetTextWithoutNotify(R.ToString());
        R_text.text = R_inputField.text;
        R_Slider.SetValueWithoutNotify(R);
        G_inputField.SetTextWithoutNotify(G.ToString());
        G_text.text = G_inputField.text;
        G_Slider.SetValueWithoutNotify(G);
        B_inputField.SetTextWithoutNotify(B.ToString());
        B_text.text = B_inputField.text;
        B_Slider.SetValueWithoutNotify(B);
        ColorImage.color = new Color(R / 255, G / 255, B / 255);
        S = tmpS; V = tmpV;
        CanvasMakerSetting();
        S = tmpS; V = tmpV;
        CanvasSetting();
        S = tmpS; V = tmpV;
    }

    public void R_inputFieldChange()
    {
        if (0 <= int.Parse(R_inputField.text) && int.Parse(R_inputField.text) <= 255)
        {

        }
        else if (int.Parse(R_inputField.text) > 255)
        {
            R_inputField.text = "255";
        }
        else
        {
            R_inputField.text = "0";
        }
        R = int.Parse(R_inputField.text);
        R_Slider.SetValueWithoutNotify(R);
        R_text.text = R_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255, 1);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }

    public void G_inputFieldChange()
    {
        if (0 <= int.Parse(G_inputField.text) && int.Parse(G_inputField.text) <= 255)
        {

        }
        else if (int.Parse(G_inputField.text) > 255)
        {
            G_inputField.text = "255";
        }
        else
        {
            G_inputField.text = "0";
        }
        G = int.Parse(G_inputField.text);
        G_Slider.SetValueWithoutNotify(G);
        G_text.text = G_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255, 1);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }

    public void B_inputFieldChange()
    {
        if (0 <= int.Parse(B_inputField.text) && int.Parse(B_inputField.text) <= 255)
        {

        }
        else if (int.Parse(B_inputField.text) > 255)
        {
            B_inputField.text = "255";
        }
        else
        {
            B_inputField.text = "0";
        }
        B = int.Parse(B_inputField.text);
        B_Slider.SetValueWithoutNotify(B);
        B_text.text = B_inputField.text;
        ColorImage.color = new Color(R / 255, G / 255, B / 255, 1);
        RGBToHSV();
        H_Slider.SetValueWithoutNotify(H);
        CanvasMakerSetting();
        CanvasSetting();
    }
}