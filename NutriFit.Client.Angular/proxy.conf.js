module.exports = {
  "/api": {
    target:
      process.env["services__nutrifit-backend-for-frontend__https__0"] ||
      process.env["services__nutrifit-backend-for-frontend__http__0"],
    secure: process.env["NODE_ENV"] !== "development",
  },
};
