import { createContext, useState, useEffect, ReactNode } from 'react';

export const AuthContext = createContext<any>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<any>(null);
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
  const [cartCount, setCartCount] = useState(0);

  useEffect(() => {
    // Nếu có token, lấy thông tin user từ localStorage (hoặc gọi API verify token)
    if (token) {
      const storedUser = localStorage.getItem('user');
      if (storedUser) {
        setUser(JSON.parse(storedUser));
        fetchCartCount(token);
      }
    }
  }, [token]);

  const fetchCartCount = async (currentToken: string) => {
    try {
      const res = await fetch('https://localhost:7054/api/Cart', {
        headers: { 'Authorization': `Bearer ${currentToken}` }
      });
      const data = await res.json();
      if (data.success) {
        setCartCount(data.data.length); // Hoặc tính tổng item.Count
      }
    } catch (e) {
      console.error(e);
    }
  };

  const login = (userData: any, jwtToken: string) => {
    setUser(userData);
    setToken(jwtToken);
    localStorage.setItem('token', jwtToken);
    localStorage.setItem('user', JSON.stringify(userData));
    fetchCartCount(jwtToken);
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    setCartCount(0);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  return (
    <AuthContext.Provider value={{ user, token, cartCount, setCartCount, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
