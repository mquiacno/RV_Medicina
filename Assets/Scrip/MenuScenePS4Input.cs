using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(InputSystemUIInputModule))]
public class MenuScenePS4Input : MonoBehaviour
{
    private InputSystemUIInputModule modulo;
    private InputActionAsset accionesLocales;

    private InputActionReference movimientoOriginal;
    private InputActionReference confirmarOriginal;
    private InputActionReference movimientoLocal;
    private InputActionReference confirmarLocal;

    private void Awake()
    {
        modulo = GetComponent<InputSystemUIInputModule>();

        movimientoOriginal = modulo.move;
        confirmarOriginal = modulo.submit;

        accionesLocales =
            ScriptableObject.CreateInstance<InputActionAsset>();

        var mapa = new InputActionMap("MenuScene");
        accionesLocales.AddActionMap(mapa);

        var movimiento = CopiarAccion(
            mapa,
            movimientoOriginal,
            "Movimiento",
            InputActionType.PassThrough
        );

        movimiento.expectedControlType = "Vector2";

        var confirmar = CopiarAccion(
            mapa,
            confirmarOriginal,
            "Confirmar",
            InputActionType.Button
        );

        // Seleccionar botones con joystick o cruceta.
        AgregarBinding(movimiento, "<Gamepad>/leftStick");
        AgregarBinding(movimiento, "<Gamepad>/dpad");

        // Activar el boton seleccionado con la X del PS4.
        AgregarBinding(confirmar, "<Gamepad>/buttonSouth");

        movimientoLocal = InputActionReference.Create(movimiento);
        confirmarLocal = InputActionReference.Create(confirmar);

        modulo.move = movimientoLocal;
        modulo.submit = confirmarLocal;
    }

    private static InputAction CopiarAccion(
        InputActionMap mapa,
        InputActionReference referencia,
        string nombre,
        InputActionType tipo)
    {
        InputAction original =
            referencia != null ? referencia.action : null;

        InputAction copia = mapa.AddAction(
            nombre,
            original != null ? original.type : tipo,
            interactions: original?.interactions,
            processors: original?.processors
        );

        if (original != null)
        {
            foreach (InputBinding binding in original.bindings)
            {
                copia.AddBinding(binding);
            }
        }

        return copia;
    }

    private static void AgregarBinding(
        InputAction accion,
        string ruta)
    {
        foreach (InputBinding binding in accion.bindings)
        {
            if (string.Equals(
                binding.effectivePath,
                ruta,
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        accion.AddBinding(ruta);
    }

    private void OnDestroy()
    {
        if (modulo != null)
        {
            if (modulo.move == movimientoLocal)
            {
                modulo.move = movimientoOriginal;
            }

            if (modulo.submit == confirmarLocal)
            {
                modulo.submit = confirmarOriginal;
            }
        }

        if (movimientoLocal != null)
        {
            Destroy(movimientoLocal);
        }

        if (confirmarLocal != null)
        {
            Destroy(confirmarLocal);
        }

        if (accionesLocales != null)
        {
            accionesLocales.Disable();
            Destroy(accionesLocales);
        }
    }
}