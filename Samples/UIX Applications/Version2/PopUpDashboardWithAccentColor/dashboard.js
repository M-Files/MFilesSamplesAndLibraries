async function OnNewDashboard(dashboard) {
  const accentColor = await MFiles.GetAccentColor()
  document
    .querySelector(':root')
    .style.setProperty('--scrollbar-thumb-color', accentColor)
  document.getElementById('accentColorValue').innerHTML = accentColor
}
