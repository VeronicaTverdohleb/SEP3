package org.example.model;

import org.json.simple.JSONObject;

public interface Model {
    JSONObject getVendors(String ingredientName);
}
