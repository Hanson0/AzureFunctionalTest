/// \file uart_internal.h
/// \brief This header defines functions internal to the UART API and should not be included
/// directly; include applibs/uart.h instead.
#pragma once

#include <stdint.h>

#ifdef __cplusplus
extern "C" {
#endif

/// <summary>
///    Struct for versioning support. Refer to UART_Config and the appropriate uart_config_v[n].h
///    header for actual struct fields.
/// </summary>
struct z__UART_Config_Base {
    /// <summary>Internal version field.</summary>
    uint32_t z__magicAndVersion;
};

/// <summary>
///    Magic value for versioning support.
/// </summary>
#define z__UART_CONFIG_MAGIC 0xde2a0000

/// <summary>
///    Versioning support for UART_InitConfig. Do not use this directly; use UART_InitConfig
///    instead.
/// </summary>
/// <param name="uartConfig">
///     Pointer to a UART_Config cast as z__UART_Config_Base object, which will be populated
///     with the appropriate default UART settings.
/// </param>
/// <param name="uartConfigStructVersion">
///     UART_Config struct version.
/// </param>
/// <returns>
///     Returns 0 if the config struct was initialized successfully, or -1 otherwise. Sets errno in
///     the event of an error.
/// </returns>
int z__UART_InitConfig(struct z__UART_Config_Base *uartConfig, uint32_t uartConfigStructVersion);

/// <summary>
///     Initializes a UART_Config struct with the default UART settings.
/// </summary>
/// <param name="uartConfig">
///     Pointer to a UART_Config object, which will be populated with the default UART settings.
/// </param>
static inline void UART_InitConfig(UART_Config *uartConfig)
{
    z__UART_InitConfig((struct z__UART_Config_Base *)uartConfig, UART_STRUCTS_VERSION);
}

/// <summary>
///    Versioning support for UART_Open. Do not use this directly; use UART_Open instead.
/// </summary>
/// <param name="uartId">
///     The identifier of the UART to be opened.
/// </param>
/// <param name="uartConfig">
///     Pointer to a UART_Config object cast as z__UART_Config_Base defining the requested
///     configuration for the UART.
/// </param>
/// <returns>
///     Returns the file descriptor of the UART device if if was opened successfully, or -1
///     otherwise. Sets errno in the event of an error.
/// </returns>
int z__UART_Open(UART_Id uartId, const struct z__UART_Config_Base *uartConfig);

/// <summary>
///     Opens the specified UART (Universal Asynchronous Receiver/Transmitter) with the given
///     configuration, and returns a file descriptor to use for subsequent calls.
///     <para>Opens the UART for exclusive access.</para>
/// </summary>
/// <param name="uartId">
///     The identifier of the UART to be opened.
/// </param>
/// <param name="uartConfig">
///     Pointer to a UART_Config object defining the requested configuration for the UART.
///     Call UART_InitConfig to get a config struct populated with the default settings.
/// </param>
/// <returns>
///     Returns the file descriptor of the UART device if if was opened successfully, or -1
///     otherwise. Sets errno in the event of an error.
/// </returns>
static inline int UART_Open(UART_Id uartId, const UART_Config *uartConfig)
{
    return z__UART_Open(uartId, (const struct z__UART_Config_Base *)uartConfig);
}

#ifdef __cplusplus
}
#endif
