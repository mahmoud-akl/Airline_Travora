export const formatEgyptTime = (dateString) => {
  if (!dateString) return '-';
  try {
    // Ensure the date is treated as UTC if it doesn't already have timezone info
    const utcString = dateString.endsWith('Z') || dateString.includes('+') ? dateString : dateString + 'Z';
    const date = new Date(utcString);
    return date.toLocaleString('en-US', { 
      timeZone: 'Africa/Cairo',
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  } catch {
    return '-';
  }
};

export const formatEgyptDate = (dateString) => {
  if (!dateString) return '-';
  try {
    const utcString = dateString.endsWith('Z') || dateString.includes('+') ? dateString : dateString + 'Z';
    const date = new Date(utcString);
    return date.toLocaleDateString('en-US', { 
      timeZone: 'Africa/Cairo',
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  } catch {
    return '-';
  }
};

export const toUtcIsoString = (localDateString) => {
  if (!localDateString) return null;
  try {
    // If it already has Z or offset, parse it directly
    if (localDateString.endsWith('Z') || localDateString.includes('+')) {
      return new Date(localDateString).toISOString();
    }
    
    // Otherwise, treat the datetime-local string as Egypt local time
    const dummyDate = new Date(localDateString + 'Z');
    const formatter = new Intl.DateTimeFormat('en-US', {
      timeZone: 'Africa/Cairo',
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false
    });
    
    const parts = formatter.formatToParts(dummyDate);
    const partMap = {};
    parts.forEach(p => partMap[p.type] = p.value);
    
    let hour = partMap.hour;
    if (hour === '24') hour = '00';
    
    const formattedCairoStr = `${partMap.year}-${partMap.month}-${partMap.day}T${hour}:${partMap.minute}:${partMap.second}Z`;
    const formattedCairoDate = new Date(formattedCairoStr);
    
    const offsetMs = formattedCairoDate.getTime() - dummyDate.getTime();
    const utcDate = new Date(dummyDate.getTime() - offsetMs);
    return utcDate.toISOString();
  } catch (error) {
    console.error('Error parsing to UTC ISO:', error);
    return new Date(localDateString).toISOString();
  }
};

export const toEgyptDateTimeLocal = (dateString) => {
  if (!dateString) return '';
  try {
    const utcString = dateString.endsWith('Z') || dateString.includes('+') ? dateString : dateString + 'Z';
    const date = new Date(utcString);
    
    const formatter = new Intl.DateTimeFormat('en-US', {
      timeZone: 'Africa/Cairo',
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    });
    
    const parts = formatter.formatToParts(date);
    const partMap = {};
    parts.forEach(p => partMap[p.type] = p.value);
    
    let hour = partMap.hour;
    if (hour === '24') hour = '00';
    
    return `${partMap.year}-${partMap.month}-${partMap.day}T${hour}:${partMap.minute}`;
  } catch (error) {
    console.error('Error formatting to Egypt datetime-local:', error);
    return '';
  }
};


