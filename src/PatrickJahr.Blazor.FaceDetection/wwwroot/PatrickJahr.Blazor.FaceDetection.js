export function isSupported() {
    return "FaceDetector" in globalThis;
}

export async function detectFaces(element) {
    const faceDetector = new FaceDetector({fastMode: false});
    try {
        return await faceDetector.detect(element);
    } catch (e) {
        console.error("Face detection failed:", e);
    }
    return [];
}
