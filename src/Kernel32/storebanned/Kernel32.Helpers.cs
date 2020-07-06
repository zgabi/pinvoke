﻿// Copyright © .NET Foundation and Contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <content>
    /// Methods and nested types that are not strictly P/Invokes but provide
    /// a slightly higher level of functionality to ease calling into native code.
    /// </content>
    public static partial class Kernel32
    {
        /// <summary>Retrieves information about the first process encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     The first <see cref="PROCESSENTRY32" /> if there was any or <see langword="null" /> otherwise (No values in
        ///     the snapshot).
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static PROCESSENTRY32? Process32First(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = PROCESSENTRY32.Create();
            if (Process32First(hSnapshot, ref entry))
            {
                return entry;
            }

            var lastError = GetLastError();
            if (lastError != Win32ErrorCode.ERROR_NO_MORE_FILES)
            {
                throw new Win32Exception(lastError);
            }

            return null;
        }

        /// <summary>Retrieves information about the next process encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     The next <see cref="PROCESSENTRY32" /> if there was any or <see langword="null" /> otherwise (No more values
        ///     in the snapshot).
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static PROCESSENTRY32? Process32Next(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = PROCESSENTRY32.Create();
            if (Process32Next(hSnapshot, ref entry))
            {
                return entry;
            }

            var lastError = GetLastError();
            if (lastError != Win32ErrorCode.ERROR_NO_MORE_FILES)
            {
                throw new Win32Exception(lastError);
            }

            return null;
        }

        /// <summary>Retrieves information about next process encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     An enumeration of all the <see cref="PROCESSENTRY32" /> present in the snapshot.
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static IEnumerable<PROCESSENTRY32> Process32Enumerate(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = Process32First(hSnapshot);

            while (entry.HasValue)
            {
                yield return entry.Value;
                entry = Process32Next(hSnapshot);
            }
        }

        /// <summary>Retrieves information about the first module encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     The first <see cref="MODULEENTRY32" /> if there was any or <see langword="null" /> otherwise (No values in
        ///     the snapshot).
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static MODULEENTRY32? Module32First(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = MODULEENTRY32.Create();
            if (Module32First(hSnapshot, ref entry))
            {
                return entry;
            }

            var lastError = GetLastError();
            if (lastError != Win32ErrorCode.ERROR_NO_MORE_FILES)
            {
                throw new Win32Exception(lastError);
            }

            return null;
        }

        /// <summary>Retrieves information about the next module encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     The next <see cref="MODULEENTRY32" /> if there was any or <see langword="null" /> otherwise (No more values
        ///     in the snapshot).
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static MODULEENTRY32? Module32Next(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = MODULEENTRY32.Create();
            if (Module32Next(hSnapshot, ref entry))
            {
                return entry;
            }

            var lastError = GetLastError();
            if (lastError != Win32ErrorCode.ERROR_NO_MORE_FILES)
            {
                throw new Win32Exception(lastError);
            }

            return null;
        }

        /// <summary>Retrieves information about next module encountered in a system snapshot.</summary>
        /// <param name="hSnapshot">
        ///     A handle to the snapshot returned from a previous call to the
        ///     <see cref="CreateToolhelp32Snapshot" /> function.
        /// </param>
        /// <returns>
        ///     An enumeration of all the <see cref="MODULEENTRY32" /> present in the snapshot.
        /// </returns>
        /// <exception cref="Win32Exception">Thrown if any error occurs.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="hSnapshot" /> is <see langword="null" />.</exception>
        public static IEnumerable<MODULEENTRY32> Module32Enumerate(SafeObjectHandle hSnapshot)
        {
            if (hSnapshot == null)
            {
                throw new ArgumentNullException(nameof(hSnapshot));
            }

            var entry = Module32First(hSnapshot);

            while (entry.HasValue)
            {
                yield return entry.Value;
                entry = Module32Next(hSnapshot);
            }
        }

        public static unsafe string QueryFullProcessImageName(
            SafeObjectHandle hProcess,
            QueryFullProcessImageNameFlags dwFlags = QueryFullProcessImageNameFlags.None)
        {
            // If we ever resize over this value something got really wrong
            const int maximumRealisticSize = 1 * 1024 * 2014;

            int size = 255;
            do
            {
                fixed (char* buffer = new char[size])
                {
                    bool success = QueryFullProcessImageName(hProcess, dwFlags, buffer, ref size);
                    if (success)
                    {
                        return new string(buffer, 0, size);
                    }

                    var lastError = GetLastError();
                    if (lastError != Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
                    {
                        lastError.ThrowOnError();
                    }
                }

                size *= 2;
            }
            while (size < maximumRealisticSize);

            throw new InvalidOperationException(
                $"QueryFullProcessImageName is expecting a buffer of more than {maximumRealisticSize} bytes");
        }

        public static bool IsWow64Process(SafeObjectHandle hProcess)
        {
            bool result;
            if (!IsWow64Process(hProcess, out result))
            {
                throw new Win32Exception();
            }

            return result;
        }

        /// <summary>
        /// Allocates the specified number of bytes from the heap.
        /// </summary>
        /// <param name="uFlags">
        /// The memory allocation attributes. The default is the <see cref="LocalAllocFlags.LMEM_FIXED"/> value. This parameter can be one or more of the following values, except for the incompatible combinations that are specifically noted.
        /// </param>
        /// <param name="uBytes">The number of bytes to allocate. If this parameter is zero and the <paramref name="uFlags"/> parameter specifies <see cref="LocalAllocFlags.LMEM_MOVEABLE"/>, the function returns a handle to a memory object that is marked as discarded.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the newly allocated memory object.
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        public static unsafe void* LocalAlloc(LocalAllocFlags uFlags, int uBytes)
        {
            // The length of SIZE_T and IntPtr vary with the bitness of the process.
            // For convenience to callers we want to expose the size of a memory allocation
            // as an Int32 no matter the process bitness. So safely 'upscale' the Int32 to
            // the appropriate IntPtr and call the native overload.
            return LocalAlloc(uFlags, new IntPtr(uBytes));
        }

        /// <summary>
        /// Changes the size or the attributes of a specified local memory object. The size can increase or decrease.
        /// </summary>
        /// <param name="hMem">A handle to the local memory object to be reallocated. This handle is returned by either the <see cref="LocalAlloc(LocalAllocFlags, IntPtr)"/> or <see cref="LocalReAlloc(void*, IntPtr, LocalReAllocFlags)"/> function.</param>
        /// <param name="uBytes">The new size of the memory block, in bytes. If uFlags specifies <see cref="LocalReAllocFlags.LMEM_MODIFY"/>, this parameter is ignored.</param>
        /// <param name="uFlags">
        /// The reallocation options. If <see cref="LocalReAllocFlags.LMEM_MODIFY"/> is specified, the function modifies the attributes of the memory object only (the uBytes parameter is ignored.) Otherwise, the function reallocates the memory object.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the reallocated memory object.
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// If LocalReAlloc fails, the original memory is not freed, and the original handle and pointer are still valid.
        /// If LocalReAlloc reallocates a fixed object, the value of the handle returned is the address of the first byte of the memory block. To access the memory, a process can simply cast the return value to a pointer.
        /// </remarks>
        public static unsafe void* LocalReAlloc(void* hMem, int uBytes, LocalReAllocFlags uFlags)
        {
            // The length of SIZE_T and IntPtr vary with the bitness of the process.
            // For convenience to callers we want to expose the size of a memory allocation
            // as an Int32 no matter the process bitness. So safely 'upscale' the Int32 to
            // the appropriate IntPtr and call the native overload.
            return LocalReAlloc(hMem, new IntPtr(uBytes), uFlags);
        }

        /// <summary>
        ///     Asynchronously sends a control code directly to a specified device driver, causing the corresponding device to perform the
        ///     corresponding operation.
        /// </summary>
        /// <param name="hDevice">
        ///     A handle to the device on which the operation is to be performed. The device is typically a
        ///     volume, directory, file, or stream. To retrieve a device handle, use the CreateFile function.
        /// </param>
        /// <param name="dwIoControlCode">
        ///     The control code for the operation. This value identifies the specific operation to be performed and the type of
        ///     device on which to perform it.
        ///     <para>
        ///         For a list of the control codes, see Remarks. The documentation for each control code provides usage details
        ///         for the <paramref name="inBuffer" /> and <paramref name="outBuffer" /> parameters.
        ///     </para>
        /// </param>
        /// <param name="inBuffer">
        ///     A pointer to the input buffer that contains the data required to perform the operation. The format of this data
        ///     depends on the value of the <paramref name="dwIoControlCode" /> parameter.
        ///     <para>
        ///         This parameter can be NULL if <paramref name="dwIoControlCode" /> specifies an operation that does not
        ///         require input data.
        ///     </para>
        /// </param>
        /// <param name="outBuffer">
        ///     A pointer to the output buffer that is to receive the data returned by the operation. The format of this data
        ///     depends on the value of the <paramref name="dwIoControlCode" /> parameter.
        ///     <para>
        ///         This parameter can be NULL if <paramref name="dwIoControlCode" /> specifies an operation that does not return
        ///         data.
        ///     </para>
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> which can be used to cancel the asynchronous operation.
        /// </param>
        /// <returns>
        ///     A <see cref="ValueTask{UInt32}"/> which returns the size of the data stored in the output buffer, in bytes, or an exception.
        /// </returns>
        /// <typeparam name="TInput">
        ///     The type which represents the data required to perform the operation. This depends on the value of the
        ///     <paramref name="dwIoControlCode" /> parameter.
        /// </typeparam>
        /// <typeparam name="TOutput">
        ///     The type which represents the data returned by the operation. This depends on the value of the
        ///     <paramref name="dwIoControlCode" /> parameter.
        /// </typeparam>
        public static unsafe ValueTask<uint> DeviceIoControlAsync<TInput, TOutput>(
            SafeObjectHandle hDevice,
            uint dwIoControlCode,
            Memory<TInput> inBuffer,
            Memory<TOutput> outBuffer,
            CancellationToken cancellationToken)
            where TInput : unmanaged
            where TOutput : unmanaged
        {
            var overlapped = new DeviceIOControlOverlapped<TInput, TOutput>(inBuffer, outBuffer);
            var nativeOverlapped = overlapped.Pack();

            bool result = Kernel32.DeviceIoControl(
                hDevice: hDevice,
                dwIoControlCode: (int)dwIoControlCode,
                inBuffer: overlapped.InputHandle.Pointer,
                nInBufferSize: inBuffer.Length * sizeof(TInput),
                outBuffer: overlapped.OutputHandle.Pointer,
                nOutBufferSize: outBuffer.Length * sizeof(TOutput),
                out int bytesReturned,
                (OVERLAPPED*)nativeOverlapped);

            if (result)
            {
                // The operation completed synchronously
                overlapped.Unpack();
                return new ValueTask<uint>((uint)bytesReturned);
            }
            else
            {
                var error = (Win32ErrorCode)Marshal.GetLastWin32Error();

                if (error != Win32ErrorCode.ERROR_IO_PENDING)
                {
                    overlapped.Unpack();
#if NET45
                    return new ValueTask<uint>(Task.Run(new Func<uint>(() => throw new Win32Exception(error))));
#else
                    return new ValueTask<uint>(Task.FromException<uint>(new PInvoke.Win32Exception(error)));
#endif
                }
                else
                {
                    return new ValueTask<uint>(overlapped.Completion);
                }
            }
        }

        /// <summary>
        /// Retrieves the results of an overlapped operation on the specified file, named pipe, or communications device.
        /// To specify a timeout interval or wait on an alertable thread, use GetOverlappedResultEx.
        /// </summary>
        /// <param name="hFile">
        /// A handle to the file, named pipe, or communications device. This is the same handle that was
        /// specified when the overlapped operation was started by a call to the ReadFile, WriteFile, ConnectNamedPipe,
        /// TransactNamedPipe, DeviceIoControl, or WaitCommEvent function.
        /// </param>
        /// <param name="lpOverlapped">
        /// A pointer to an <see cref="NativeOverlapped" /> structure that was specified when the overlapped
        /// operation was started.
        /// </param>
        /// <param name="lpNumberOfBytesTransferred">
        /// A pointer to a variable that receives the number of bytes that were actually
        /// transferred by a read or write operation. For a TransactNamedPipe operation, this is the number of bytes that were read
        /// from the pipe. For a DeviceIoControl operation, this is the number of bytes of output data returned by the device
        /// driver. For a ConnectNamedPipe or WaitCommEvent operation, this value is undefined.
        /// </param>
        /// <param name="bWait">
        /// If this parameter is TRUE, and the Internal member of the lpOverlapped structure is STATUS_PENDING,
        /// the function does not return until the operation has been completed. If this parameter is FALSE and the operation is
        /// still pending, the function returns FALSE and the <see cref="GetLastError" /> function returns
        /// <see cref="Win32ErrorCode.ERROR_IO_INCOMPLETE" />.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero.To get extended error information, call
        /// <see cref="GetLastError" />.
        /// </para>
        /// </returns>
        /// <remarks>
        /// The results reported by the GetOverlappedResult function are those of the specified handle's last overlapped operation
        /// to which the specified <see cref="NativeOverlapped" /> structure was provided, and for which the operation's results were
        /// pending. A pending operation is indicated when the function that started the operation returns FALSE, and the
        /// GetLastError function returns <see cref="Win32ErrorCode.ERROR_IO_PENDING" />. When an I/O operation is pending, the
        /// function that started the operation resets the hEvent member of the <see cref="NativeOverlapped" /> structure to the
        /// nonsignaled state. Then when the pending operation has been completed, the system sets the event object to the signaled
        /// state.
        /// <para>
        /// If the bWait parameter is TRUE, GetOverlappedResult determines whether the pending operation has been completed
        /// by waiting for the event object to be in the signaled state.
        /// </para>
        /// <para>
        /// If the hEvent member of the <see cref="NativeOverlapped" /> structure is NULL, the system uses the state of the hFile
        /// handle to signal when the operation has been completed. Use of file, named pipe, or communications-device handles for
        /// this purpose is discouraged. It is safer to use an event object because of the confusion that can occur when multiple
        /// simultaneous overlapped operations are performed on the same file, named pipe, or communications device. In this
        /// situation, there is no way to know which operation caused the object's state to be signaled.
        /// </para>
        /// </remarks>
        [NoFriendlyOverloads]
        public static unsafe bool GetOverlappedResult(
            SafeObjectHandle hFile,
            NativeOverlapped* lpOverlapped,
            out int lpNumberOfBytesTransferred,
            bool bWait)
        {
            return GetOverlappedResult(hFile, (OVERLAPPED*)lpOverlapped, out lpNumberOfBytesTransferred, bWait);
        }
    }
}
