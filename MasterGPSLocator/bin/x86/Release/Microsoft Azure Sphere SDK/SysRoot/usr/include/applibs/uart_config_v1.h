/// \file uart_config_v1.h
/// \brief This header contains the v1 definition of the UART configuration struct and
/// associated types.
///
/// You should not include this header directly; include applibs/uart.h instead.
#pragma once

#include <stdint.h>
#include <stdlib.h>

typedef uint32_t UART_BaudRate_Type;
typedef uint8_t UART_BlockingMode_Type;
typedef uint8_t UART_DataBits_Type;
typedef uint8_t UART_Parity_Type;
typedef uint8_t UART_StopBits_Type;
typedef uint8_t UART_FlowControl_Type;

/// <summary>
/// A struct defining the configuration options for a UART. Call
/// UART_InitConfig to initialize an instance.
/// </summary>
struct z__UART_Config_v1 {
    /// <summary>Internal version field. Do not edit.</summary>
    uint32_t z__magicAndVersion;
    /// <summary>Baud rate of the UART.</summary>
    UART_BaudRate_Type baudRate;
    /// <summary>Blocking mode setting for the UART.</summary>
    UART_BlockingMode_Type blockingMode;
    /// <summary>Data bits setting for the UART.</summary>
    UART_DataBits_Type dataBits;
    /// <summary>Parity setting for the UART.</summary>
    UART_Parity_Type parity;
    /// <summary>Stop bits setting for the UART.</summary>
    UART_StopBits_Type stopBits;
    /// <summary>Flow control setting for the UART.</summary>
    UART_FlowControl_Type flowControl;
};

/// <summary>
/// An enum defining the valid values for UART blocking or non-blocking modes
/// </summary>
typedef enum {
    /// <summary>Reads and writes to the file handle will be non-blocking and will return an
    /// error if the call would block. Reads may return less data than requested.</summary>
    UART_BlockingMode_NonBlocking = 0,
} UART_BlockingMode;

/// <summary>
/// An enum defining the valid values for UART data bits.
/// </summary>
typedef enum {
    /// <summary>Eight data bits.</summary>
    UART_DataBits_Eight = 8
} UART_DataBits;

/// <summary>
/// An enum defining the valid values for UART parity.
/// </summary>
typedef enum {
    /// <summary>No parity bit.</summary>
    UART_Parity_None = 0
} UART_Parity;

/// <summary>
/// An enum defining the valid values for UART stop bits.
/// </summary>
typedef enum {
    /// <summary>One stop bit.</summary>
    UART_StopBits_One = 1
} UART_StopBits;

/// <summary>
/// An enum defining the valid values for flow control.
/// settings.
/// </summary>
typedef enum {
    /// <summary>No flow control.</summary>
    UART_FlowControl_None = 0,
    /// <summary>Enable RTS/CTS hardware flow control.</summary>
    UART_FlowControl_RTSCTS = 1,
    /// <summary>Enable XON/XOFF software flow control.</summary>
    UART_FlowControl_XONXOFF = 2
} UART_FlowControl;
