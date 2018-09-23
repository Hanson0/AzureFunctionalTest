/// \file wificonfig_internal.h
/// \brief This header contains internal functions for Wi-Fi configuration.
///
/// These functions are not thread safe.
#pragma once

#include <stdint.h>
#include <sys/types.h>

#ifdef __cplusplus
extern "C" {
#endif
/// <summary>
///    Struct for WifiConfig_StoredNetwork versioning support. Refer to
///    z__WifiConfig_StoredNetwork_v[n] in the appropriate wificonfig_structs_v[n].h header for
///    actual struct fields.
/// </summary>
struct z__WifiConfig_StoredNetwork_Base {
    /// <summary>Internal version field.</summary>
    uint32_t z__magicAndVersion;
};

/// <summary>
///    Magic value for versioning support for WifiConfig stored network struct.
/// </summary>
#define z__WIFICONFIG_STORED_NETWORK_STRUCT_MAGIC 0xb22e0000

/// <summary>
///    Struct for WifiConfig_ConnectedNetwork versioning support. Refer to
///    z__WifiConfig_ConnectedNetwork_v[n] in the appropriate wificonfig_structs_v[n].h header for
///    actual struct fields.
/// </summary>
struct z__WifiConfig_ConnectedNetwork_Base {
    /// <summary>Internal version field.</summary>
    uint32_t z__magicAndVersion;
};

/// <summary>
///    Magic value for versioning support for WifiConfig connected network struct.
/// </summary>
#define z__WIFICONFIG_CONNECTED_NETWORK_STRUCT_MAGIC 0x09ef0000

/// <summary>
///    Struct for WifiConfig_ScannedNetwork versioning support. Refer to
///    z__WifiConfig_ScannedNetwork_v[n] in the appropriate wificonfig_structs_v[n].h header for
///    actual struct fields.
/// </summary>
struct z__WifiConfig_ScannedNetwork_Base {
    /// <summary>Internal version field.</summary>
    uint32_t z__magicAndVersion;
};

/// <summary>
///    Magic value for versioning support for WifiConfig scanned network struct.
/// </summary>
#define z__WIFICONFIG_SCANNED_NETWORK_STRUCT_MAGIC 0x7eb10000

/// <summary>
///    Versioning support for WifiConfig_ForgetNetwork. Do not use this directly; use
///    WifiConfig_ForgetNetwork instead.
/// </summary>
/// <param name="storedNetwork">
///     Pointer to a WifiConfig_StoredNetwork object cast as z__WifiConfig_StoredNetwork_Base
///     defining the Wi-Fi network to forget.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int z__WifiConfig_ForgetNetwork(const struct z__WifiConfig_StoredNetwork_Base *storedNetwork);

/// <summary>
///     Forget an existing stored Wi-Fi network from the device. This disconnects the device from
///     the network if the provided network is currently the connected network.
///     <para>The caller must provide a struct that identifies the stored Wi-Fi network to forget.
///     </para>
///     <para>Returns:</para>
///     <para>0 for success, -1 for error.</para>
/// </summary>
/// <param name="storedNetwork">
///     Pointer to a <see cref="WifiConfig_StoredNetwork"/> struct with details of the stored Wi-Fi
///     network to forget.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static inline int WifiConfig_ForgetNetwork(const WifiConfig_StoredNetwork *storedNetwork)
{
    return z__WifiConfig_ForgetNetwork(
        (const struct z__WifiConfig_StoredNetwork_Base *)storedNetwork);
}

/// <summary>
///    Versioning support for WifiConfig_GetStoredNetworks. Do not use this directly; use
///    WifiConfig_GetStoredNetworks instead.
/// </summary>
/// <param name="storedNetworkArray">
///     Pointer to an array of WifiConfig_StoredNetwork objects cast as
///     z__WifiConfig_StoredNetwork_Base. The array is populated with the stored Wi-Fi networks on
///     success.
/// </param>
/// <param name="storedNetworkArrayCount">
///     Number of elements the array can hold. The array should have one element for each stored
///     Wi-Fi network.
/// </param>
/// <param name="storedNetworkStructVersion">
///     WifiConfig_StoredNetwork struct version.
/// </param>
/// <returns>
///     Number of WifiConfig_StoredNetwork elements populated in the output array when successful,
///     or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
ssize_t z__WifiConfig_GetStoredNetworks(struct z__WifiConfig_StoredNetwork_Base *storedNetworkArray,
                                        size_t storedNetworkArrayCount,
                                        uint32_t storedNetworkStructVersion);

/// <summary>
///     Retrieve all stored Wi-Fi networks by populating a stored networks array.
///     <para>The caller must provide an array to be populated with the stored Wi-Fi networks. If
///     the array is too small to hold all the Wi-Fi networks, the function fills all elements
///     provided and returns the number of filled elements.</para>
///     <para>Returns:</para>
///     <para>Number of WifiConfig_StoredNetwork elements filled in the output array when
///     successful, or -1 for error.</para>
/// </summary>
/// <param name="storedNetworkArray">
///     Pointer to an array of WifiConfig_StoredNetwork objects to fill with stored Wi-Fi network
///     information.
/// </param>
/// <param name="storedNetworkArrayCount">
///     Number of elements the array can hold. The array should have one element for each stored
///     Wi-Fi network.
/// </param>
/// <returns>
///     Number of WifiConfig_StoredNetwork elements populated in the output array when successful,
///     or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static inline ssize_t WifiConfig_GetStoredNetworks(WifiConfig_StoredNetwork *storedNetworkArray,
                                                   size_t storedNetworkArrayCount)
{
    return z__WifiConfig_GetStoredNetworks(
        (struct z__WifiConfig_StoredNetwork_Base *)storedNetworkArray, storedNetworkArrayCount,
        WIFICONFIG_STRUCTS_VERSION);
}

/// <summary>
///    Versioning support for WifiConfig_GetCurrentNetwork. Do not use this directly; use
///    WifiConfig_GetCurrentNetwork instead.
/// </summary>
/// <param name="connectedNetwork">
///     Pointer to a WifiConfig_ConnectedNetwork cast as z__WifiConfig_ConnectedNetwork_Base
///     object, which is populated with properties for the currently connected Wi-Fi network.
/// </param>
/// <param name="ConnectedNetworkStructVersion">
///     WifiConfig_ConnectedNetwork struct version.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int z__WifiConfig_GetCurrentNetwork(struct z__WifiConfig_ConnectedNetwork_Base *connectedNetwork,
                                    uint32_t ConnectedNetworkStructVersion);

/// <summary>
///     Retrieve a struct that describes the currently connected Wi-Fi network.
///     <para>The caller must provide a struct to populate with properties for the currently
///     connected Wi-Fi network. If the device is not currently connected to any network, -1 is
///     returned, in which case errno is set to ENOTCONN(107).
///     </para>
///     <para>Returns:</para>
///     <para>0 for success, -1 for error.</para>
/// </summary>
/// <param name="connectedNetwork">
///     Struct that describes the currently connected Wi-Fi network.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static inline int WifiConfig_GetCurrentNetwork(WifiConfig_ConnectedNetwork *connectedNetwork)
{
    return z__WifiConfig_GetCurrentNetwork(
        (struct z__WifiConfig_ConnectedNetwork_Base *)connectedNetwork, WIFICONFIG_STRUCTS_VERSION);
}

/// <summary>
///    Versioning support for WifiConfig_GetScannedNetworks. Do not use this directly; use
///    WifiConfig_GetScannedNetworks instead.
/// </summary>
/// <param name="scannedNetworkArray">
///     Pointer to an array of WifiConfig_ScannedNetwork objects cast as
///     z__WifiConfig_ScannedNetwork_Base. The array is populated with the scanned Wi-Fi networks
///     found by the last scan operation on success.
/// </param>
/// <param name="scannedNetworkArrayCount">
///     The number of elements the array can hold. The array should have one element for each Wi-Fi
///     network found by the last scan operation.
/// </param>
/// <param name="scannedNetworkStructVersion">
///     WifiConfig_ScannedNetwork struct version.
/// </param>
/// <returns>
///     Number of WifiConfig_ScannedNetwork elements written in the output array when successful, or
///     -1 in the case of failure, in which case errno is set to the error.
/// </returns>
ssize_t z__WifiConfig_GetScannedNetworks(
    struct z__WifiConfig_ScannedNetwork_Base *scannedNetworkArray, size_t scannedNetworkArrayCount,
    uint32_t scannedNetworkStructVersion);

/// <summary>
///     Return the Wi-Fi networks found by the last scan operation.
///     <para>The caller must provide an array to receive all scanned networks found by the last
///     scan operation. If the array isn't large enough to hold all the networks scanned, the
///     function fills the array and returns the number of filled elements.
///     </para>
///     <para>Returns:</para>
///     <para>The number of WifiConfig_ScannedNetwork elements populated in the output array when
///     successful, or -1 error.</para>
/// </summary>
/// <param name="scannedNetworkArray">
///     Pointer to an array to fill with scanned Wi-Fi network information.
/// </param>
/// <param name="scannedNetworkArrayCount">
///     Number of elements the array can hold. The array should have one element for each Wi-Fi
///     network found by the last scan operation.
/// </param>
/// <returns>
///     Number of WifiConfig_ScannedNetwork elements written in the output array when successful, or
///     -1 in the case of failure, in which case errno is set to the error.
/// </returns>
static inline ssize_t WifiConfig_GetScannedNetworks(WifiConfig_ScannedNetwork *scannedNetworkArray,
                                                    size_t scannedNetworkArrayCount)
{
    return z__WifiConfig_GetScannedNetworks(
        (struct z__WifiConfig_ScannedNetwork_Base *)scannedNetworkArray, scannedNetworkArrayCount,
        WIFICONFIG_STRUCTS_VERSION);
}

#ifdef __cplusplus
}
#endif
