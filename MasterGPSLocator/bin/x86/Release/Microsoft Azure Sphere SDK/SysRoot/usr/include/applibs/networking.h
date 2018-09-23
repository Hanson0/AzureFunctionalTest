/// \file networking.h
/// \brief This header defines an interface for determining whether network connectivity is
/// available. Calls to this API are thread-safe.
#pragma once

#include <stdbool.h>

#ifdef __cplusplus
extern "C" {
#endif

/// <summary>
///     Queries whether networking is ready, i.e. whether internet connectivity is available and the
///     time is synced.
///     <para> **Errors** </para>
///     In case of errors, this function will return -1 and set errno to the error value.
///     <para>EFAULT: the provided <paramref name="outIsNetworkingReady"/> is NULL.</para>
///     Any other errno may also be specified; such errors are not deterministic and no guarantee
///     is made that the same behaviour will be retained through system updates.
/// </summary>
/// <param name="outIsNetworkingReady">
///      A pointer to a boolean to which the result is written. This will be set to true if
///      networking is ready, and false otherwise.
/// </param>
/// <returns>
///     0 for success, or -1 in the case of failure, in which case errno is set to the error.
/// </returns>
int Networking_IsNetworkingReady(bool *outIsNetworkingReady);

#ifdef __cplusplus
}
#endif
