export function isSupported() {
  return "FaceDetector" in globalThis;
}

export async function detectFaces(element) {
  const faceDetector = new FaceDetector({ fastMode: false });

  try {
    const wrapper = document.getElementById("wrapper");
    const faces = await faceDetector.detect(element);
    faces.forEach((face) => {
      console.log(face);
      const { top, left, width, height } = face.boundingBox;
      const faceDiv = document.createElement("div");
      faceDiv.className = "face";
      Object.assign(faceDiv.style, {
        width: `${width}px`,
        height: `${height}px`,
        left: `${left}px`,
        top: `${top}px`,
      });
      wrapper.appendChild(faceDiv);
    });
    return faces.length;
  } catch (e) {
    console.error("Face detection failed:", e);
  }
  return 0;
}
