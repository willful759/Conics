using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conics : MonoBehaviour
{
    public enum Type {
      Line,
      Circle,
      Ellipse,
      Parabola,
      Hyperbola
    }

    public Color color;
    public Type type;
    public Text conicName;

    public Slider sliderA, sliderB;
    public Text slrNameA, slrNameB;

    public Transform t1, t2;

    Vector3 p1;
    Vector3 p2;

    public float a { get; set; } = 1;

    public float b { get; set; } = 1;

    int _samples;
    public int samples {
      get {
        return _samples;
      }
      set {
        // this helps with handleing the hyperbola
        _samples = (value + 4) - (value % 4);
      }
    }

    LineRenderer linerenderer;

    delegate Vector3 Sampler(float t);

    // Start is called before the first frame update
    void Start()
    {
      samples = 1000;
      linerenderer = GetComponent<LineRenderer>();
      linerenderer.positionCount = samples + 1;
      linerenderer.startColor = color;
      linerenderer.endColor = color;
      ShowSlidersAndPoints();
    }

    // Update is called once per frame
    void Update()
    {
      p1 = new Vector3(t1.position.x, t1.position.y, 0);
      p2 = new Vector3(t2.position.x, t2.position.y, 0);
      GeneratePoints();
    }

    public void SetType(string t){
      /* 
       * Come on Unity, you can set the type on the editor but not
       * when using OnClick() ?
       */
      switch (t) {
        case "l":
          type = Type.Line;
          break;
        case "c":
          type = Type.Circle;
          break;
        case "e":
          type = Type.Ellipse;
          break;
        case "p":
          type = Type.Parabola;
          break;
        case "h":
          type = Type.Hyperbola;
          break;
      }
      ShowSlidersAndPoints();
    }

    void Sample(Sampler f){
      linerenderer.SetPosition(0, f(0));
      for(int i = 1; i <= samples; i++){
        linerenderer.SetPosition(i, f((float)i/samples));
      }
    }
    
    void ShowSlidersAndPoints() {
      sliderA.gameObject.SetActive(true);
      sliderB.gameObject.SetActive(true);
      slrNameB.gameObject.SetActive(true);
      slrNameA.gameObject.SetActive(true);
      t2.gameObject.SetActive(false);

      slrNameA.text = "a";
      slrNameB.text = "b";

      switch (type) {
        case Type.Line:
          sliderA.gameObject.SetActive(false);
          sliderB.gameObject.SetActive(false);

          slrNameA.gameObject.SetActive(false);
          slrNameB.gameObject.SetActive(false);

          t2.gameObject.SetActive(true);

          conicName.text = "Linea recta";
          break;
        case Type.Circle:
          sliderB.gameObject.SetActive(false);

          slrNameB.gameObject.SetActive(false);

          slrNameA.text = "r";

          conicName.text = "Circulo";
          break;
        case Type.Parabola:
          sliderB.gameObject.SetActive(false);

          slrNameB.gameObject.SetActive(false);

          slrNameA.text = "p";

          conicName.text = "Parabola";
          break;
        case Type.Ellipse:
          
          conicName.text = "Elipse";
          break;
        case Type.Hyperbola:
          
          conicName.text = "Hiperbola";
          break;
        default:
          break;
      }
    }

    void GeneratePoints(){
      float pi = Mathf.PI;
      switch (type) {
        case Type.Line:
          Sample( t => p1 + (p2 - p1)*t );
          break;
        case Type.Circle:
          Sample( t => {
              float x = a*Mathf.Cos(2*pi*t);
              float y = a*Mathf.Sin(2*pi*t);
              return p1 + new Vector3(x, y, 0);
              });
          break;
        case Type.Ellipse:
          Sample( t => {
              float x = a*Mathf.Cos(2*pi*t);
              float y = b*Mathf.Sin(2*pi*t);
              return p1 + new Vector3(x, y, 0);
              });
          break;
        case Type.Parabola:
          Sample( t => {
              //--------------------------------------------
              // Prevents the parabola from trying to drawing
              // at infinity
              if ( t == 0f ) { t += 1e4f; }
              if ( t == 1f ) { t -= 1e4f; }
              //--------------------------------------------
              float x = 2*a/Mathf.Tan(-pi*t);
              float y = a*Mathf.Pow(1/Mathf.Tan(-pi*t), 2);
              return p1 + new Vector3(x, y, 0);
              });
          break;
        case Type.Hyperbola:
          Sample( t => {
              //--------------------------------------------
              // Prevents the hyperbola from drawing weridly
              if ( t == 0.25f ) { t -= 1e4f; }
              if ( t == 0.75f ) { t += 1e4f; }
              if ( t > 0.25f && t < 0.75f ) { t = 1 - t; }
              //--------------------------------------------
              float x = a/Mathf.Cos(2*pi*t);
              float y = -b*Mathf.Tan(2*pi*t);
              return p1 + new Vector3(x, y, 0);
              });
          break;
        default:
          break;
      }
    }
}
