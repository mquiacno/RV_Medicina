# RV_Medicina

Aplicación de Realidad Virtual (RV) desarrollada en Unity para fines educativos/médicos.

## Descripción

RV_Medicina es una experiencia interactiva en realidad virtual orientada a la simulación y aprendizaje en el ámbito de medicina. El proyecto incluye un menú principal y una escena interactiva con interacción VR mediante el XR Interaction Toolkit.

## Características

- **Menú principal**: Navegación entre escenas con opciones para iniciar y salir.
- **Soporte VR**: Integración con XR Interaction Toolkit y samples (Starter Assets, Device Simulator).
- **Controles adaptados**: 
  - `GyroLook.cs` - Control de cámara/orientación basado en giroscopio
  - `GamepadMove.cs` - Movimiento con gamepad
  - `VirtualHands.cs` - Gestión de manos virtuales
- **Escenas**:
  - `MenuScene` - Escena de menú principal
  - `SampleScene` - Escena principal de la simulación

## Tecnologías

- [Unity](https://unity.com/) - Motor de desarrollo
- [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@latest) - Interacción VR
- C# - Lógica de juego/aplicación

## Requisitos

- Unity (compatible con la versión especificada en `ProjectSettings/ProjectVersion.txt`)
- Dispositivo VR compatible o simulador XR

## Estructura del Proyecto

```
Assets/
├── Scrip/          # Scripts C# personalizados
├── Scenes/         # Escenas de Unity (MenuScene, SampleScene)
├── XRI/            # Configuración XR Interaction Toolkit
├── Ui/             # Elementos de interfaz
└── ... 
```

## Ejecución

1. Abrir el proyecto con Unity Hub
2. Cargar la escena `MenuScene` o `SampleScene`
3. Ejecutar en modo Play con el dispositivo VR configurado

## Equipo

- [@mquiacno](https://github.com/mquiacno) - Autor principal
- [@carltofen1](https://github.com/carltofen1) - Autor principal
- [@ALDREZZ2](https://github.com/ALDREZZ2) - Autor principal
```