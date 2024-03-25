const timer = (ms) => new Promise((res) => setTimeout(res, ms));

async function detectBarcode(image, formats) {
  if (!formats) {
    formats = await supportedFormats();
  }

  const barcodeDetector = new BarcodeDetector({ formats });
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

async function detectBarcodeFromVideo(video, formats) {
  const stream = await navigator.mediaDevices.getUserMedia({
    video: {
      facingMode: {
        ideal: "environment",
      },
    },
    audio: false,
  });
  video.srcObject = stream;
  await video.play();

  const barcodeDetector = new BarcodeDetector({ formats });
  let detection = true;
  let codes = [];
  while (detection) {
    const barcodes = await barcodeDetector.detect(video);
    if (barcodes.length > 0) {
      detection = false;
      codes = barcodes.map((barcode) => barcode.rawValue);
    } else {
      await timer(1000);
    }
  }
  const tracks = stream.getTracks();
  tracks.forEach((track) => {
    track.stop();
    stream.removeTrack(track);
  });

  video.pause();
  // video.srcObject = "";
  return codes;
}

export function isSupported() {
  return !!("BarcodeDetector" in globalThis);
}

export async function supportedFormats() {
  const supportedFormats = await BarcodeDetector.getSupportedFormats();
  supportedFormats.forEach((format) => console.log(format));
  return supportedFormats;
}

export async function detectCode(element, formats) {
  try {
    if (!formats) {
      formats = await supportedFormats();
    }

    if (element instanceof HTMLVideoElement || element instanceof VideoFrame) {
      return detectBarcodeFromVideo(element, formats);
    }

    if (element instanceof File) {
      const imageRef = await createImageBitmap(element);
      const codes = await detectBarcode(imageRef, formats);
      return codes;
    }
    return detectBarcode(element, formats);
  } catch (err) {
    console.error("Failed to detect barcodes. Error:", err);
  }
  return [];
}
