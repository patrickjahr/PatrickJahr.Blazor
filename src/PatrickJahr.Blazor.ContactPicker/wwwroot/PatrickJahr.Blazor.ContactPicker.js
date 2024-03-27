export function isSupported() {
  return !!("contacts" in navigator && "ContactsManager" in window);
}

async function checkProperties() {
  const supportedProperties = await navigator.contacts.getProperties();
  console.log(supportedProperties);
  return supportedProperties;
}

async function getContacts(props = null, multiple = true) {
  try {
    const supportedProperties =
      props ?? (await navigator.contacts.getProperties());
    const contacts = await navigator.contacts.select(supportedProperties, {
      multiple,
    });
    return contacts;
  } catch (ex) {
    console.error(ex);
    return [];
  }
}
