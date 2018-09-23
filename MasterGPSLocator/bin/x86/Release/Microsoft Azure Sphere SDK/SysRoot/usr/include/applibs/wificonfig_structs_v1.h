/// \file wificonfig_structs_v1.h
/// \brief This header contains data structures and enums for Wi-Fi configuration.
///
#pragma once

#include <stdint.h>
#include <stdbool.h>

#ifdef __cplusplus
extern "C" {
#endif

/// <summary>
///     Defines the maximum number of bytes for Wi-Fi SSID.
/// </summary>
#define WIFICONFIG_SSID_MAX_LENGTH 32
/// <summary>
///     Defines the maximum number of characters for Wi-Fi WPA2 key. The key can be either a
///     passphrase of 8-63 ASCII characters or 64 hexadecimal digits.
/// </summary>
#define WIFICONFIG_WPA2_KEY_MAX_BUFFER_SIZE 64
/// <summary>
///     Defines the buffer size for Wi-Fi BSSID.
/// </summary>
#define WIFICONFIG_BSSID_BUFFER_SIZE 6

/// <summary>
///     Represents the security setting of a Wi-Fi network.
/// </summary>
typedef enum {
    /// <summary>Unknown security setting.</summary>
    WifiConfig_Security_Unknown = 0,
    /// <summary>Security setting without key management.</summary>
    WifiConfig_Security_Open = 1,
    /// <summary>Security setting using WPA2 pre-shared key.</summary>
    WifiConfig_Security_Wpa2_Psk = 2,
} WifiConfig_Security;

typedef uint8_t WifiConfig_Security_Type;

/// <summary>
///     Struct holding properties of a scanned Wi-Fi network, which represents a 802.11 Basic
///     Service Set (BSS).
/// </summary>
struct z__WifiConfig_ScannedNetwork_v1 {
    /// <summary>Magic and version to uniquely identify a specific version of struct.</summary>
    uint32_t z__magicAndVersion;
    /// <summary>Fixed length buffer for SSID.</summary>
    uint8_t ssid[WIFICONFIG_SSID_MAX_LENGTH];
    /// <summary>Fixed length buffer for BSSID.</summary>
    uint8_t bssid[WIFICONFIG_BSSID_BUFFER_SIZE];
    /// <summary>Number of bytes for SSID.</summary>
    uint8_t ssidLength;
    /// <summary>Enum WifiConfig_Security for security setting.</summary>
    WifiConfig_Security_Type security;
    /// <summary>BSS center frequency in MHz.</summary>
    uint32_t frequencyMHz;
    /// <summary>Signal strength in RSSI.</summary>
    int8_t signalRssi;
};

/// <summary>
///     Struct holding properties of stored Wi-Fi network, which represents a 802.11 Service Set.
/// </summary>
struct z__WifiConfig_StoredNetwork_v1 {
    /// <summary>Magic and version to uniquely identify a specific version of struct.</summary>
    uint32_t z__magicAndVersion;
    /// <summary>Fixed length buffer for SSID.</summary>
    uint8_t ssid[WIFICONFIG_SSID_MAX_LENGTH];
    /// <summary>Number of bytes for SSID.</summary>
    uint8_t ssidLength;
    /// <summary>Boolean representing whether the network is enabled.</summary>
    bool isEnabled;
    /// <summary>Boolean representing whether the network is connected.</summary>
    bool isConnected;
    /// <summary>Enum WifiConfig_Security for security setting.</summary>
    WifiConfig_Security_Type security;
};

/// <summary>
///     Struct holding properties of current Wi-Fi network, which represent a 802.11 Basic Service
///     Set (BSS).
/// </summary>
struct z__WifiConfig_ConnectedNetwork_v1 {
    /// <summary>Magic and version to uniquely identify a specific version of struct.</summary>
    uint32_t z__magicAndVersion;
    /// <summary>Fixed length buffer for SSID.</summary>
    uint8_t ssid[WIFICONFIG_SSID_MAX_LENGTH];
    /// <summary>Fixed length buffer for BSSID.</summary>
    uint8_t bssid[WIFICONFIG_BSSID_BUFFER_SIZE];
    /// <summary>Number of bytes for SSID.</summary>
    uint8_t ssidLength;
    /// <summary>Enum WifiConfig_Security for security setting.</summary>
    WifiConfig_Security_Type security;
    /// <summary>BSS center frequency in MHz.</summary>
    uint32_t frequencyMHz;
    /// <summary>Signal strength in RSSI.</summary>
    int8_t signalRssi;
};

#ifdef __cplusplus
}
#endif
