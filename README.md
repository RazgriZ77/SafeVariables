SafeVariables

## Información sobre el paquete
Este paquete contiene un script (SafeVariables.cs) que ayuda a asegurar variables importantes, ofuscando su valor en memoria. Es importante recalcar que no va a evitar que se pueda hackear, pero sí complica la faena.

También posee un encriptador de valores de PlayerPrefs (SafeEncrypt.cs) para guardar clases y múltiples tipos de variables con el uso de una encriptación AES.

## Ejemplo de Variables Ofuscadas
```C#
using UnityEngine;
using CustomTool.SafeVariables;

public class TestClass : MonoBehaviour {
    // El valor de estas variables no son alterables en memoria.
    private SafeInt score = new SafeInt(0);
    private SafeFloat gameTime = new SafeFloat(0f);
    private SafeBool hasWon = new SafeBool(false);

    private int scoreToWin = 5;
    private float timeStamp = 0;
    private float timeStampAdd = 5;

    private void Start() {
        score = new SafeInt(0);
        gameTime = new SafeFloat(0f);
        hasWon = new SafeBool(false);

        UpdateTimeStamp();
    }

    private void Update() {
        ScoreByTime();
    }

    private void ScoreByTime() {
        if (hasWon.Value) return;

        // Para alterar una SafeVariable, hay que acceder a su valor usando la variable 'Value'
        gameTime.Value += Time.deltaTime;

        if (gameTime.Value >= timeStamp) {
            score.Value++;

            if (score.Value >= scoreToWin) hasWon.Value = true;
            else UpdateTimeStamp();
        }
    }

    private void UpdateTimeStamp() {
        timeStamp += timeStampAdd;
    }
}
```
