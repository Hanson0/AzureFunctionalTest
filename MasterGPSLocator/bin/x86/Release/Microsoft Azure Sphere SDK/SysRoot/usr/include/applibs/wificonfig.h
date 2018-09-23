/// \file wificonfig.h
/// \brief This header contains functions for Wi-Fi configuration. These functions are only
/// permitted if the application has the WifiConfig capability in its application manifest.
///
/// To use these functions, define WIFICONFIG_STRUCTS_VERSION in your source code as the version of
/// the structures you are using. Currently, the only valid version is 1.
///    \c \#define WIFICONFIG_STRUCTS_VERSION 1
/// Thereafter, you can use the "friendly" names of the WifiConfig_ structures, which start with
/// WifiConfig_.
///
/// These functions are not thread safe.
#pragma once

#include <stdint.h>
#include <sys/types.h>

#include <applibs/wificonfig_structs_v1.h>

#if WIFICONFIG_STRUCTS_VERSION == 1
typedef struct z__WifiConfig_StoredNetwork_v1 WifiConfig_StoredNetwork;
typedef struct z__WifiConfig_ConnectedNetwork_v1 WifiConfig_ConnectedNetwork;
typedef struct z__WifiConfig_ScannedNetwork_v1 WifiConfig_ScannedNetwork;
#else
#error To use applibs/wificonfig.h you must first #define WIFICONFIG_STRUCTS_VERSION
#endif

#include <applibs/wificonfig_internal.h>

#ifdef __cplusplus
extern "C" {
#endif

/// <summary>
///     Store an open Wi-Fi network with the provided SSID and without any key.
///     <para>The caller must provide the SSID for the Wi-Fi network. This function will not succeed
///     if a stored Wi-Fi network that has the same SSID and no key already exists, see the error
///     section (EEXIST). If a network that has the same SSID and a key already exists, the function
///     stores the network on the device.</para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EEXIST: a stored Wi-Fi network that has the same SSID and no key already
///     exists.</para>
///     <para>EFAULT: the <paramref name="ssid"/> is NULL.</para>
///     <para>ERANGE: the <paramref name="ssidLength"/> is 0 or grater than <see
///     cref="WIFICONFIG_SSID_MAX_LENGTH"/>.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee is
///     made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="ssid">
///     Pointer to an SSID byte array with unspecified character encoding that identifies the open
///     Wi-Fi network to store.
/// </param>
/// <param name="ssidLength">
///     Number of bytes in the SSID of the open Wi-Fi network to store.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int WifiConfig_StoreOpenNetwork(const uint8_t *ssid, size_t ssidLength);

/// <summary>
///     Store a Wi-Fi network using a WPA2 key with the provided SSID and pre-shared key.
///     <para>The caller must provide the SSID and pre-shared key for the network to be created and
///     stored. This will not succeed if a stored Wi-Fi network already exists that has the same
///     SSID and uses WPA2, see the error section(EEXIST). If an existing network has the same SSID
///     but a different key management setting, the function stores the network on the device.
///     </para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EEXIST: a stored Wi-Fi network already exists that has the same SSID and uses
///     WPA2.</para>
///     <para>EFAULT: the <paramref name="ssid"> or <paramref name="psk"/> is NULL.</para>
///     <para>ERANGE: the <paramref name="ssidLength"/> or <paramref name="pskLength"/> is 0 or
///     grater than <see cref="WIFICONFIG_WPA2_KEY_MAX_BUFFER_SIZE"/> and <see
///     cref="WIFICONFIG_SSID_MAX_LENGTH"/>.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee is
///     made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="ssid">
///     Pointer to an SSID byte array with unspecified character encoding that identifies the WPA2
///     Wi-Fi network to store.
/// </param>
/// <param name="ssidLength">
///     Number of bytes in the <paramref name="ssid"> of the WPA2 Wi-Fi network to store.
/// </param>
/// <param name="psk">
///     Pointer to a buffer that contains the WPA2 pre-shared key for the WPA2 Wi-Fi network to
///     store.
/// </param>
/// <param name="pskLength">
///     Length of the WPA2 pre-shared key for the WPA2 Wi-Fi network to store.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int WifiConfig_StoreWpa2Network(const uint8_t *ssid, size_t ssidLength, const char *psk,
                                size_t pskLength);

/// <summary>
///     Forget an existing Wi-Fi network that is stored on the device. This has the effect of
///     disconnecting the device from the network if the specified network is currently the
///     connected network.
///     <para>The caller must provide a struct that describes the stored Wi-Fi network to
///     forget. </para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EFAULT: the <paramref name="storedNetwork"/> is NULL.</para>
///     <para>ENOENT: the <paramref name="storedNetwork"/> does not match any of the stored
///     networks.</para>
///     <para>EINVAL: the <paramref name="storedNetwork"/> or its struct version is invalid.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee is
///     made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="storedNetwork">
///     Pointer to a <see cref="WifiConfig_StoredNetwork"/> struct with details of the stored Wi-Fi
///     network to forget.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static int WifiConfig_ForgetNetwork(const WifiConfig_StoredNetwork *storedNetwork);

/// <summary>
///     Forget all stored Wi-Fi networks. This has the effect of disconnecting the device from the
///     network if it is currently connected to a network.
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee
///     is made that the same behaviour will be retained through system updates.
/// </summary>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int WifiConfig_ForgetAllNetworks(void);

/// <summary>
///     Get the number of stored Wi-Fi networks.
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee
///     is made that the same behaviour will be retained through system updates.
/// </summary>
/// <returns>
///     Number of stored networks when successful, or -1 in the case of failure, in which case errno
///     is set to the error.
/// </returns>
ssize_t WifiConfig_GetStoredNetworkCount(void);

/// <summary>
///     Retrieve all stored Wi-Fi networks by populating a stored networks array.
///     <para>The caller must provide an array in which to return the stored Wi-Fi networks. If the
///     array is too small to hold all the stored Wi-Fi networks, the function fills the array and
///     returns the number of filled elements.</para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EFAULT: the <paramref name="storedNetworkArray"/> is NULL.</para>
///     <para>ERANGE: the <paramref name="storedNetworkArrayCount"/> is 0.</para>
///     <para>EINVAL: the <paramref name="storedNetworkArray"/> struct version is invalid.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee
///     is made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="storedNetworkArray">
///     A pointer to an array to populate with stored Wi-Fi networks.
/// </param>
/// <param name="storedNetworkArrayCount">
///     Number of elements the <paramref name="storedNetworkArray"/> can hold. The array should have
///     one element for each stored Wi-Fi network.
/// </param>
/// <returns>
///     Number of WifiConfig_StoredNetwork elements populated in the output array when successful,
///     or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static ssize_t WifiConfig_GetStoredNetworks(WifiConfig_StoredNetwork *storedNetworkArray,
                                            size_t storedNetworkArrayCount);

/// <summary>
///     Retrieve a struct that contains properties of the currently connected Wi-Fi network.
///     <para>The caller must provide a struct to be populated with properties for the currently
///     connected Wi-Fi network.</para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EFAULT: the <paramref name="connectedNetwork"/> is NULL.</para>
///     <para>ENOTCONN: the device is not currently connected to any network.</para>
///     <para>EINVAL: the <paramref name="connectedNetwork"/> or its struct version is
///     invalid.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and
///     no guarantee is made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="connectedNetwork">
///     A pointer to a struct in which to return the currently connected Wi-Fi network.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static int WifiConfig_GetCurrentNetwork(WifiConfig_ConnectedNetwork *connectedNetwork);

/// <summary>
///     Trigger a scan to find available Wi-Fi networks and return the number of networks found.
///     <para>This is a blocking call.</para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee
///     is made that the same behaviour will be retained through system updates.
/// </summary>
/// <returns>
///     The number of networks found when successful, or -1 in the case of failure, in which case
///     errno is set to the error.
/// </returns>
ssize_t WifiConfig_TriggerScanAndGetScannedNetworkCount(void);

/// <summary>
///     Return the Wi-Fi networks found by the last scan operation.
///     <para>The caller must provide an array to receive all the networks found by the last scan
///     operation. If the array is too small to hold all the networks, the function fills all the
///     elements and returns the number of filled elements. </para>
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EACCES: the application manifest does not include the WifiConfig capability.</para>
///     <para>EFAULT: the <paramref name="scannedNetworkArray"/> is NULL.</para>
///     <para>ERANGE: the <paramref name="scannedNetworkArrayCount"/> is 0.</para>
///     <para>EINVAL: the <paramref name="scannedNetworkArray"/> or its struct version is
///     invalid. </para>
///     <para>EAGAIN: the Wi-Fi device isn't ready yet.</para>
///     Any other errno may also be specified; such errors are not deterministic and
///     no guarantee is made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="scannedNetworkArray">
///     A pointer to an array to populate with Wi-Fi network information.
/// </param>
/// <param name="scannedNetworkArrayCount">
///     The number of elements the array can hold. The array should have one element for each Wi-Fi
///     network found by the last scan operation.
/// </param>
/// <returns>
///     The number of WifiConfig_ScannedNetwork elements filled in the output array when successful,
///     or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static ssize_t WifiConfig_GetScannedNetworks(WifiConfig_ScannedNetwork *scannedNetworkArray,
                                             size_t scannedNetworkArrayCount);

#ifdef __cplusplus
}
#endif
