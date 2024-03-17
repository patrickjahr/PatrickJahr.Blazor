export function getBarcodeBlobPromise() {
  return new Promise(async (res, rej) => {
    try {
      const logoResponse = await fetch("./barcode.png");
      res(logoResponse.blob());
    } catch (err) {
      rej(err);
    }
  });
}
