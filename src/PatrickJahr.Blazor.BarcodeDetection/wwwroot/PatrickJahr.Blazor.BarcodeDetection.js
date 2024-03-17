export function isSupported() {
    return !!("BarcodeDetector" in globalThis);
}

export async function supportedFormats() {
    const supportedFormats = await BarcodeDetector.getSupportedFormats();
    supportedFormats.forEach((format) => console.log(format));
    return supportedFormats;
}

export async function detectBarcode(image, formats) {
    if (!formats) {
        formats = await supportedFormats();
    }

    if (!image) {
        throw new Error("No image provided");
    }

    const barcodeDetector = new BarcodeDetector({
        formats,
    });
    try {
        let barcodes = await barcodeDetector.detect(image);
        if (Array.isArray(barcodes) && barcodes.length > 0) {
            barcodes = barcodes.map((barcode) => barcode.rawValue);
        }
        return barcodes;
    } catch (err) {
        console.log(err);
    }

    return [];
}