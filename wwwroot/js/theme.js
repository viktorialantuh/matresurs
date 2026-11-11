// Публичная тема загружается из theme.json. Токенов GitHub в браузере нет.
window.matresursTheme = (() => {
  const fallback = {
    Title:"МатРесурс", Primary:"#366cf0", Accent:"#13b8a7",
    Sidebar:"#10213f", Background:"#f3f7fc", Font:"system", Radius:16
  };
  let published = {...fallback};
  const hex = /^#[0-9a-fA-F]{6}$/;
  const clean = source => {
    const x = (source && typeof source === "object") ? source : {};
    const t = {...fallback};
    for (const key of ["Primary","Accent","Sidebar","Background"])
      if (typeof x[key] === "string" && hex.test(x[key])) t[key] = x[key];
    if (typeof x.Title === "string" && x.Title.trim().length > 0)
      t.Title = x.Title.trim().slice(0,48);
    if (["system","arial","georgia"].includes(x.Font)) t.Font = x.Font;
    if (Number.isInteger(x.Radius)) t.Radius = Math.max(8,Math.min(26,x.Radius));
    return t;
  };
  const apply = source => {
    const t = clean(source);
    const root = document.documentElement;
    root.style.setProperty("--mr-primary",t.Primary);
    root.style.setProperty("--mr-accent",t.Accent);
    root.style.setProperty("--mr-sidebar",t.Sidebar);
    root.style.setProperty("--mr-background",t.Background);
    root.style.setProperty("--mr-radius",t.Radius+"px");
    root.style.setProperty("--mr-font", t.Font === "georgia" ? "Georgia, 'Times New Roman', serif" :
      t.Font === "arial" ? "Arial, Helvetica, sans-serif" :
      "system-ui, -apple-system, 'Segoe UI', Arial, sans-serif");
    document.title = t.Title+" — учёт материалов";
    return t;
  };
  return {
    load: async () => {
      try {
        const response = await fetch(new URL("theme.json", document.baseURI),{cache:"no-store"});
        if(response.ok) published = clean(await response.json());
      } catch(error) { console.warn("Общая тема недоступна; используется стандартная.",error); }
      apply(published);
      return {...published};
    },
    preview: source => apply(source),
    restore: () => apply(published)
  };
})();
