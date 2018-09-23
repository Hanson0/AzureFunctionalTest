/// \file uart.h
/// \brief This header contains the functions and types needed to open and use a UART
/// (Universal Asynchronous Receiver/Transmitter) on the device. Access to individual UARTs is
/// restricted based on the UART field of the application's manifest.
///
/// Please define UART_STRUCTS_VERSION to the appropriate version when using this header.
#pragma once

#include <applibs/uart_config_v1.h>

#if UART_STRUCTS_VERSION == 1
typedef struct z__UART_Config_v1 UART_Config;
#else
#error To use applibs/uart.h you must first #define UART_STRUCTS_VERSION to 1
#endif

/// <summary>
///     Specifies the type of a UART ID, which is used to specify a UART peripheral instance.
/// </summary>
typedef int UART_Id;

#include <applibs/uart_internal.h>

#ifdef __cplusplus
extern "C" {
#endif

/// <summary>
///     Initializes a UART_Config struct with the default UART settings.
/// </summary>
/// <param name="uartConfig">
///     Pointer to a UART_Config object, which will be populated with the default UART settings.
/// </param>
static inline void UART_InitConfig(UART_Config *uartConfig);

/// <summary>
///     Opens the specified UART with the given configuration, and returns a file descriptor to use
///     for subsequent calls.
///     <para>Opens the UART for exclusive access.</para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: access to <see cref="UART_Id"/> is not permitted as the <paramref
///     name="uartId"/> is not listed in the Uart field of the application manifest.</para>
///     <para>ENODEV: the <paramref name="uartId"/> is invalid.</para>
///     <para>EINVAL: the <paramref name="uartConfig"/> represents an invalid configuration.</para>
///     <para>EBUSY: the <paramref name="uartId"/> is already open.</para>
///     <para>EFAULT: the <paramref name="uartConfig"/> is NULL.</para>
///     Any other errno may also be specified; such errors are not deterministic and
///     no guarantee is made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="uartId">
///     The identifier of the <see cref="UART_Id"/> to be opened.
/// </param>
/// <param name="uartConfig">
///     Pointer to a UART_Config object defining the requested configuration for the UART.
///     Call <see cref="UART_InitConfig"/> to get a config struct populated with the default
///     settings.
/// </param>
/// <returns>
///     Returns the file descriptor of the UART device if if was opened successfully, or -1
///     otherwise. Sets errno in the event of an error.
/// </returns>
int UART_Open(UART_Id uartId, const UART_Config *uartConfig);

#ifdef __cplusplus
}
#endif
