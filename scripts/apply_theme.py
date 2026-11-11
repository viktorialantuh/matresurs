#!/usr/bin/env python3
"""Validate a site theme submitted through an issue by the repository owner."""
import json
import os
import re
from pathlib import Path

event = json.loads(Path(os.environ["GITHUB_EVENT_PATH"]).read_text(encoding="utf-8"))
issue = event["issue"]
repo_owner = event["repository"]["owner"]["login"]
if issue["user"]["login"].casefold() != repo_owner.casefold():
    raise SystemExit("Only the repository owner can publish a theme.")
if not issue["title"].startswith("[MatResurs theme]"):
    raise SystemExit("Not a theme issue.")
body = issue.get("body") or ""
prefix = "MATRESURS_THEME_V1\n"
if not body.startswith(prefix):
    raise SystemExit("Invalid theme request format.")
raw = json.loads(body[len(prefix):])
if not isinstance(raw, dict):
    raise SystemExit("Theme must be an object.")
allowed = {"Title", "Primary", "Accent", "Sidebar", "Background", "Font", "Radius"}
if set(raw) != allowed:
    raise SystemExit("Unexpected theme keys.")
title = raw["Title"]
if not isinstance(title, str) or not (1 <= len(title.strip()) <= 48) or any(c in title for c in "\r\n"):
    raise SystemExit("Invalid site title.")
for key in ("Primary", "Accent", "Sidebar", "Background"):
    value = raw[key]
    if not isinstance(value, str) or not re.fullmatch(r"#[0-9a-fA-F]{6}", value):
        raise SystemExit(f"Invalid color {key}.")
if raw["Font"] not in ("system", "arial", "georgia"):
    raise SystemExit("Invalid font.")
if type(raw["Radius"]) is not int or not 8 <= raw["Radius"] <= 26:
    raise SystemExit("Invalid radius.")
raw["Title"] = title.strip()
Path("wwwroot/theme.json").write_text(
    json.dumps(raw, ensure_ascii=False, indent=2) + "\n", encoding="utf-8"
)
print("Validated theme for", repo_owner)
