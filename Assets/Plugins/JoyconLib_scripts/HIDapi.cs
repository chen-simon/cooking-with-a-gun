using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System;
using System.Text;


public class HIDapi {
    private const string PRIMARY_DLL = "hidapi";
    private const string FALLBACK_DLL = "libhidapi";
    private static bool primaryLoaded = true;

    // Helper to try invoking the function from the primary and fallback DLLs
    private static T TryInvoke<T>(Func<T> primary, Func<T> fallback)
    {
        try
        {
            if (primaryLoaded) return primary();
        }
        catch (DllNotFoundException)
        {
            primaryLoaded = false;
        }
        return fallback();
    }

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_init", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_init_primary();
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_init", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_init_fallback();
    public static int hid_init() => TryInvoke(hid_init_primary, hid_init_fallback);

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_exit", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_exit_primary();
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_exit", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_exit_fallback();
    public static int hid_exit() => TryInvoke(hid_exit_primary, hid_exit_fallback);

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_error", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_error_primary(IntPtr device);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_error", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_error_fallback(IntPtr device);
    public static IntPtr hid_error(IntPtr device) => TryInvoke(() => hid_error_primary(device), () => hid_error_fallback(device));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_enumerate", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_enumerate_primary(ushort vendor_id, ushort product_id);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_enumerate", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_enumerate_fallback(ushort vendor_id, ushort product_id);
    public static IntPtr hid_enumerate(ushort vendor_id, ushort product_id) => TryInvoke(() => hid_enumerate_primary(vendor_id, product_id), () => hid_enumerate_fallback(vendor_id, product_id));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_free_enumeration", CallingConvention = CallingConvention.Cdecl)]
    private static extern void hid_free_enumeration_primary(IntPtr devs);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_free_enumeration", CallingConvention = CallingConvention.Cdecl)]
    private static extern void hid_free_enumeration_fallback(IntPtr devs);
    public static void hid_free_enumeration(IntPtr devs) => TryInvoke(() => { hid_free_enumeration_primary(devs); return 0; }, () => { hid_free_enumeration_fallback(devs); return 0; });

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_get_feature_report", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_feature_report_primary(IntPtr device, byte[] data, UIntPtr length);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_get_feature_report", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_feature_report_fallback(IntPtr device, byte[] data, UIntPtr length);
    public static int hid_get_feature_report(IntPtr device, byte[] data, UIntPtr length) => TryInvoke(() => hid_get_feature_report_primary(device, data, length), () => hid_get_feature_report_fallback(device, data, length));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_get_indexed_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_indexed_string_primary(IntPtr device, int string_index, StringBuilder str, UIntPtr maxlen);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_get_indexed_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_indexed_string_fallback(IntPtr device, int string_index, StringBuilder str, UIntPtr maxlen);
    public static int hid_get_indexed_string(IntPtr device, int string_index, StringBuilder str, UIntPtr maxlen) => TryInvoke(() => hid_get_indexed_string_primary(device, string_index, str, maxlen), () => hid_get_indexed_string_fallback(device, string_index, str, maxlen));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_get_manufacturer_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_manufacturer_string_primary(IntPtr device, StringBuilder str, UIntPtr maxlen);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_get_manufacturer_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_manufacturer_string_fallback(IntPtr device, StringBuilder str, UIntPtr maxlen);
    public static int hid_get_manufacturer_string(IntPtr device, StringBuilder str, UIntPtr maxlen) => TryInvoke(() => hid_get_manufacturer_string_primary(device, str, maxlen), () => hid_get_manufacturer_string_fallback(device, str, maxlen));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_get_product_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_product_string_primary(IntPtr device, StringBuilder str, UIntPtr maxlen);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_get_product_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_product_string_fallback(IntPtr device, StringBuilder str, UIntPtr maxlen);
    public static int hid_get_product_string(IntPtr device, StringBuilder str, UIntPtr maxlen) => TryInvoke(() => hid_get_product_string_primary(device, str, maxlen), () => hid_get_product_string_fallback(device, str, maxlen));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_get_serial_number_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_serial_number_string_primary(IntPtr device, StringBuilder str, UIntPtr maxlen);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_get_serial_number_string", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_get_serial_number_string_fallback(IntPtr device, StringBuilder str, UIntPtr maxlen);
    public static int hid_get_serial_number_string(IntPtr device, StringBuilder str, UIntPtr maxlen) => TryInvoke(() => hid_get_serial_number_string_primary(device, str, maxlen), () => hid_get_serial_number_string_fallback(device, str, maxlen));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_open", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_open_primary(ushort vendor_id, ushort product_id, string serial_number);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_open", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_open_fallback(ushort vendor_id, ushort product_id, string serial_number);
    public static IntPtr hid_open(ushort vendor_id, ushort product_id, string serial_number) => TryInvoke(() => hid_open_primary(vendor_id, product_id, serial_number), () => hid_open_fallback(vendor_id, product_id, serial_number));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_close", CallingConvention = CallingConvention.Cdecl)]
    private static extern void hid_close_primary(IntPtr device);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_close", CallingConvention = CallingConvention.Cdecl)]
    private static extern void hid_close_fallback(IntPtr device);
    public static void hid_close(IntPtr device) => TryInvoke(() => { hid_close_primary(device); return 0; }, () => { hid_close_fallback(device); return 0; });

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_open_path", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_open_path_primary(string path);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_open_path", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr hid_open_path_fallback(string path);
    public static IntPtr hid_open_path(string path) => TryInvoke(() => hid_open_path_primary(path), () => hid_open_path_fallback(path));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_read", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_read_primary(IntPtr device, byte[] data, UIntPtr length);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_read", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_read_fallback(IntPtr device, byte[] data, UIntPtr length);
    public static int hid_read(IntPtr device, byte[] data, UIntPtr length) => TryInvoke(() => hid_read_primary(device, data, length), () => hid_read_fallback(device, data, length));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_read_timeout", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_read_timeout_primary(IntPtr dev, byte[] data, UIntPtr length, int milliseconds);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_read_timeout", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_read_timeout_fallback(IntPtr dev, byte[] data, UIntPtr length, int milliseconds);
    public static int hid_read_timeout(IntPtr dev, byte[] data, UIntPtr length, int milliseconds) => TryInvoke(() => hid_read_timeout_primary(dev, data, length, milliseconds), () => hid_read_timeout_fallback(dev, data, length, milliseconds));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_send_feature_report", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_send_feature_report_primary(IntPtr device, byte[] data, UIntPtr length);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_send_feature_report", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_send_feature_report_fallback(IntPtr device, byte[] data, UIntPtr length);
    public static int hid_send_feature_report(IntPtr device, byte[] data, UIntPtr length) => TryInvoke(() => hid_send_feature_report_primary(device, data, length), () => hid_send_feature_report_fallback(device, data, length));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_set_nonblocking", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_set_nonblocking_primary(IntPtr device, int nonblock);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_set_nonblocking", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_set_nonblocking_fallback(IntPtr device, int nonblock);
    public static int hid_set_nonblocking(IntPtr device, int nonblock) => TryInvoke(() => hid_set_nonblocking_primary(device, nonblock), () => hid_set_nonblocking_fallback(device, nonblock));

    [DllImport(PRIMARY_DLL, EntryPoint = "hid_write", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_write_primary(IntPtr device, byte[] data, UIntPtr length);
    [DllImport(FALLBACK_DLL, EntryPoint = "hid_write", CallingConvention = CallingConvention.Cdecl)]
    private static extern int hid_write_fallback(IntPtr device, byte[] data, UIntPtr length);
    public static int hid_write(IntPtr device, byte[] data, UIntPtr length) => TryInvoke(() => hid_write_primary(device, data, length), () => hid_write_fallback(device, data, length));
}

struct hid_device_info {
    public string path;
    public ushort vendor_id;
    public ushort product_id;
    public string serial_number;
    public ushort release_number;
    public string manufacturer_string;
    public string product_string;
    public ushort usage_page;
    public ushort usage;
    public int interface_number;
    public IntPtr next;
}